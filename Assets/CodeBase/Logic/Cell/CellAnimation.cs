using System.Collections.Generic;
using CodeBase.Infrastructures.View;
using CodeBase.Logic.Field;
using CodeBase.Logic.Pool;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellAnimation : ICellAnimation
    {
        private readonly IUIViewFactory _viewFactory;
        private readonly ICellSpawner _spawner;
        private readonly GameFieldConfig _fieldConfig;
        private readonly PoolList<CellView> _pool;
        
        private Transform _parent;

        public CellAnimation(IUIViewFactory viewFactory, ICellSpawner spawner,
            GameFieldConfig fieldConfig)
        {
            _viewFactory = viewFactory;
            _spawner = spawner;
            _fieldConfig = fieldConfig;
            _pool = new PoolList<CellView>();
        }

        public void Dispose()
        {
            int lengthPool = _pool.GetLengthPool();
            for (int i = 0; i < lengthPool; i++)
            {
                _viewFactory.Destroy(_pool.GetInactiveObject(true));
            }
        }

        public void SetParentView(Transform parent)
        {
            _parent = parent;
        }

        public void SmoothTransition(CellModel from, CellModel to, bool isMerging)
        {
            var instanceView = GetCell();
            Move(from, to, instanceView, isMerging);
        }

        private CellView GetCell()
        {
            CellView view;
            
            if (_pool.HasInactiveObjects())
            {
                view = _pool.GetInactiveObject(true);
                view.transform.SetParent(_parent);
            }
            else
            {
                view = _viewFactory.Create<CellView>(ViewType.CELL, parent:_parent);
                view.Rect.sizeDelta = new Vector2(_fieldConfig.CellSize, _fieldConfig.CellSize);
                _pool.Add(view);
            }

            return view;
        }

        private void Move(CellModel from, CellModel to, CellView animationView, bool isMerging)
        {
            CellView fromView = _spawner.FieldModelView[from];
            CellView toView = _spawner.FieldModelView[to];

            Color colorCell = _fieldConfig.ColorsCells[from.Value];
            Color colorText = from.Value <= 2 ? _fieldConfig.CellDarkColor :
                _fieldConfig.CellLightColor;
            animationView.SetColor(colorCell);
            animationView.SetValue(from.Point.ToString(), colorText);

            animationView.transform.position = fromView.transform.position;

            Sequence sequence = DOTween.Sequence();
            
            sequence.Append(animationView.transform.DOMove(toView.transform.position,
                _fieldConfig.TimeMoveCell)).SetEase(Ease.InOutQuad);

            if (isMerging)
            {
                sequence.AppendCallback(() =>
                {
                    Color colorCell = _fieldConfig.ColorsCells[to.Value];
                    Color colorText = to.Value <= 2 ? _fieldConfig.CellDarkColor :
                        _fieldConfig.CellLightColor;
                    animationView.SetColor(colorCell);
                    animationView.SetValue(to.Point.ToString(), colorText);
                });

                sequence.Append(animationView.transform.DOScale(1.2f, _fieldConfig.TimeMoveCell));
                sequence.Append(animationView.transform.DOScale(1, _fieldConfig.TimeMoveCell));
            }

            sequence.AppendCallback(() =>
            {
                to.SetValue(to.X, to.Y, to.Value);
                CancelAnimation(animationView);
            });
        }

        private void CancelAnimation(CellView view)
        {
            view.ReturnToPool();
        }
    }
}