using CodeBase.Infrastructures;
using CodeBase.Infrastructures.StateMachines.Game;
using CodeBase.Logic.Field;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellGameResult : ICellGameResult
    {
        private readonly ICellSpawner _spawner;
        private readonly ICellMerge _merge;
        private readonly GameFieldConfig _fieldConfig;
        private readonly GameConfig _gameConfig;
        private readonly GameStateMachine _stateMachine;

        public CellGameResult(ICellSpawner spawner, ICellMerge merge,
            GameFieldConfig fieldConfig, GameConfig gameConfig,
            GameStateMachine stateMachine)
        {
            _spawner = spawner;
            _merge = merge;
            _fieldConfig = fieldConfig;
            _gameConfig = gameConfig;
            _stateMachine = stateMachine;
        }

        public void OnCheckGameResult()
        {
            bool lose = true;

            for (int x = 0; x < _fieldConfig.FieldSize; x++)
            {
                for (int y = 0; y < _fieldConfig.FieldSize; y++)
                {
                    CellModel model = _spawner.Field[x, y];
                    
                    if (model.Value == _gameConfig.MaxValueCell)
                    {
                        _stateMachine.Enter<GameResultState, bool>(true);
                        return;
                    }

                    if (lose)
                    {
                        if (model.IsEmpty ||
                            _merge.FindCellToMerge(model, Vector2.left) ||
                            _merge.FindCellToMerge(model, Vector2.right) ||
                            _merge.FindCellToMerge(model, Vector2.up) ||
                            _merge.FindCellToMerge(model, Vector2.down))
                        {
                            lose = false;
                        }
                    }
                }
            }
            
            if(lose)
                _stateMachine.Enter<GameResultState, bool>(false);
        }
    }
}