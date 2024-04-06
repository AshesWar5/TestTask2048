using CodeBase.Infrastructures.View;
using UnityEngine;

namespace CodeBase.Logic.Field
{
    public sealed class GameField : IGameField
    {
        private readonly IUIViewFactory _viewFactory;
        private readonly GameFieldConfig _fieldConfig;

        private View _field;

        public GameField(IUIViewFactory viewFactory, GameFieldConfig fieldConfig)
        {
            _viewFactory = viewFactory;
            _fieldConfig = fieldConfig;
        }

        public void CreateField(Transform parent)
        {
            if(_field is not null) return;
            
            float fieldWidth = _fieldConfig.FieldSize * (_fieldConfig.CellSize + _fieldConfig.Spacing) +
                               _fieldConfig.Spacing;
            _field = _viewFactory.Create<View>(ViewType.FIELD, parent:parent);
            _field.Rect.sizeDelta = new Vector2(fieldWidth, fieldWidth);
            _field.transform.position = parent.localPosition;
        }
    }
}