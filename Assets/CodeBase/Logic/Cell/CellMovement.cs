using CodeBase.Logic.Field;
using CodeBase.Services.Input_Game;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellMovement : ICellMovement
    {
        private readonly IInputService _input;
        private readonly ICellSpawner _spawner;
        private readonly GameFieldConfig _fieldConfig;
        private readonly ICellMerge _merge;
        private readonly ICellAnimation _animation;
        private readonly ICellGameResult _gameResult;

        private bool _isCellMoved;

        public CellMovement(IInputService input, ICellSpawner spawner,
            GameFieldConfig fieldConfig, ICellMerge merge, ICellAnimation animation,
            ICellGameResult gameResult)
        {
            _input = input;
            _spawner = spawner;
            _fieldConfig = fieldConfig;
            _merge = merge;
            _animation = animation;
            _gameResult = gameResult;
        }

        public void Initialize()
        {
            _input.Move += OnMoved;
        }

        public void Stop()
        {
            _input.Move -= OnMoved;
            _isCellMoved = false;
        }

        private void OnMoved(Vector2 direction)
        {
            _isCellMoved = false;
            
            _merge.ResetCellsMerge();
            Move(direction);

            if (_isCellMoved)
            {
                _spawner.GenerateRandomCell();
                _gameResult.OnCheckGameResult();
            }
        }

        private void Move(Vector2 direction)
        {
            int fieldSize = _fieldConfig.FieldSize;
            CellModel[,] models = _spawner.Field;
            
            int startXY = direction.x > 0 || direction.y < 0 ? fieldSize - 1 : 0;
            int dir = direction.x != 0 ? (int) direction.x : -(int) direction.y;

            for (int i = 0; i < fieldSize; i++)
            {
                for (int k = startXY; k >= 0 && k < fieldSize; k -= dir)
                {
                    CellModel cell = direction.x != 0 ? models[k, i] : models[i, k];
                    
                    if(cell.IsEmpty)
                        continue;
                    
                    if (_merge.FindCellToMerge(cell, direction))
                    {
                        _isCellMoved = true;
                        continue;
                    }

                    if (FindEmptyCell(cell, direction))
                        _isCellMoved = true;
                }
            }
        }
        
        private bool FindEmptyCell(CellModel cell, Vector2 direction)
        {
            CellModel emptyCell = null;
            
            CellModel[,] models = _spawner.Field;
            int fieldSize = _fieldConfig.FieldSize;
            int startX = cell.X + (int) direction.x;
            int startY = cell.Y - (int) direction.y;
            
            for (int x = startX, y = startY;
                x >= 0 && x < fieldSize && y >= 0 && y < fieldSize;
                x += (int) direction.x, y -= (int) direction.y)
            {
                CellModel model = models[x, y];
                if (model.IsEmpty)
                {
                    model.SetValue(model.X, model.Y, cell.Value, false);
                    if(!cell.IsEmpty)
                        _animation.SmoothTransition(cell, model, false);
                    cell.SetValue(cell.X, cell.Y, 0);
                    emptyCell = model;
                }
                else
                {
                    break;
                }
            }

            return emptyCell is not null;
        }
    }
}