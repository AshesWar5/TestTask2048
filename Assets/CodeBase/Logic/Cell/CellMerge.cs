using System;
using CodeBase.Logic.Field;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellMerge : ICellMerge
    {
        public event Action<int> Merged;
        
        private readonly ICellSpawner _spawner;
        private readonly ICellAnimation _animation;
        private readonly GameFieldConfig _fieldConfig;

        private int _score;

        public CellMerge(ICellSpawner spawner, ICellAnimation animation,
            GameFieldConfig fieldConfig)
        {
            _spawner = spawner;
            _animation = animation;
            _fieldConfig = fieldConfig;
        }

        public void Dispose()
        {
            _score = 0;
        }

        public void ResetCellsMerge()
        {
            if(_spawner.Field is null || _spawner.Field.Length == 0)
                return;
            
            for (int x = 0; x < _fieldConfig.FieldSize; x++)
            {
                for (int y = 0; y < _fieldConfig.FieldSize; y++)
                {
                    if(_spawner.Field[x, y] is not null)
                        _spawner.Field[x, y].ResetMerged();
                }
            }
        }

        public bool FindCellToMerge(CellModel cell, Vector2 direction)
        {
            CellModel[,] models = _spawner.Field;
            int fieldSize = _fieldConfig.FieldSize;
            int startX = cell.X + (int) direction.x;
            int startY = cell.Y - (int) direction.y;

            for (int x = startX, y = startY;
                x >= 0 && x < fieldSize && y >= 0 && y < fieldSize;
                x += (int) direction.x, y -= (int) direction.y)
            {
                CellModel model = models[x, y];
                
                if(model.IsEmpty)
                    continue;

                if (model.Value == cell.Value && !model.HasMerged)
                {
                    model.IncreaseValue();
                    _animation.SmoothTransition(cell, model, true);
                    cell.SetValue(cell.X, cell.Y, 0);

                    _score += model.Point;
                    Merged?.Invoke(_score);
                    
                    return true;
                }

                break;
            }

            return false;
        }
    }
}