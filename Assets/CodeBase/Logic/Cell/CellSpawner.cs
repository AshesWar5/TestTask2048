using System.Collections.Generic;
using CodeBase.Infrastructures.Controller;
using CodeBase.Infrastructures.View;
using CodeBase.Logic.Field;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic.Cell
{
    public sealed class CellSpawner : ICellSpawner
    {
        public CellModel[,] Field { get; private set; }
        public Dictionary<CellModel, CellView> FieldModelView { get; }
        
        private readonly IUIViewFactory _viewFactory;
        private readonly IControllerFactory _controllerFactory;
        private readonly GameFieldConfig _fieldConfig;
        private readonly List<CellController> _controllers;
        
        public CellSpawner(IUIViewFactory viewFactory,
            IControllerFactory controllerFactory, GameFieldConfig fieldConfig)
        {
            _viewFactory = viewFactory;
            _controllerFactory = controllerFactory;
            _fieldConfig = fieldConfig;
            
            FieldModelView = new Dictionary<CellModel, CellView>();
            _controllers = new List<CellController>();
        }

        public void Dispose()
        {
            for (int i = 0; i < _controllers.Count; i++)
            {
                _controllers[i].Dispose();
                _controllers[i] = null;
            }

            for (int x = 0; x < _fieldConfig.FieldSize; x++)
            {
                for (int y = 0; y < _fieldConfig.FieldSize; y++)
                {
                    _viewFactory.Destroy(FieldModelView[Field[x, y]]);
                    Field[x, y] = null;
                }
            }

            Field = null;
            _controllers.Clear();
        }

        public void CreateCells(Transform parent)
        {
            float cellSize = _fieldConfig.CellSize;
            float spacing = _fieldConfig.Spacing;
            int fieldSize = _fieldConfig.FieldSize;

            Field = new CellModel[fieldSize, fieldSize];
            
            float fieldWidth = fieldSize * (cellSize + spacing) + spacing;
            float startX = -fieldWidth / 2 + cellSize / 2 + spacing;
            float startY = fieldWidth / 2 - cellSize / 2 - spacing;
            
            for (int x = 0; x < fieldSize; x++)
            {
                for (int y = 0; y < fieldSize; y++)
                {
                    var model = new CellModel(); 
                    var view = _viewFactory.Create<CellView>(ViewType.CELL, parent:parent);
                    Vector2 position = new Vector2(startX + x * (cellSize + spacing),
                        startY - y * (cellSize + spacing));
                    view.transform.localPosition = position;
                    view.Rect.sizeDelta = new Vector2(cellSize, cellSize);
                    
                    model.SetValue(x, y, 0);

                    var controller = _controllerFactory.Create<CellController,
                        CellModel, CellView>(model, view);
                    
                    Field[x, y] = model;
                    _controllers.Add(controller);
                    FieldModelView.Add(model, view);
                }
            }

            for (int i = 0; i < _fieldConfig.StartCountSpawnCell; i++)
            {
                GenerateRandomCell();
            }
        }

        public void GenerateRandomCell()
        {
            var emptyCells = new List<CellModel>();
            for (int x = 0; x < _fieldConfig.FieldSize; x++)
            {
                for (int y = 0; y < _fieldConfig.FieldSize; y++)
                {
                    CellModel model = Field[x, y];
                    if (model.IsEmpty)
                    {
                        emptyCells.Add(model);
                    }
                }
            }

            int value = Random.Range(0, 10) == 0 ? 2 : 1;
            var cell = emptyCells[Random.Range(0, emptyCells.Count)];
            cell.SetValue(cell.X, cell.Y, value);
        }
    }
}