using CodeBase.Infrastructures.View;
using CodeBase.Logic;
using CodeBase.Logic.Cell;
using CodeBase.Logic.Field;

namespace CodeBase.Infrastructures.StateMachines.Game
{
    public sealed class LoadLevelState : State<GameStateMachine>, IPayLoadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameField _gameField;
        private readonly ICellSpawner _cellSpawner;
        private readonly ICellAnimation _animation;
        private readonly IUIPageFactory _pageFactory;
        private readonly ICellGameResult _gameResult;

        private PageView _field;

        public LoadLevelState(SceneLoader sceneLoader, GameStateMachine stateMachine,
            IGameField gameField, ICellSpawner cellSpawner, ICellAnimation animation,
            IUIPageFactory pageFactory) : base(stateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameField = gameField;
            _cellSpawner = cellSpawner;
            _animation = animation;
            _pageFactory = pageFactory;
        }

        public void Enter(string sceneName) =>
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit() { }

        private void OnLoaded()
        {
            _field ??= _pageFactory.Create<PageView>(PageType.GAME_FIELD);
            _animation.SetParentView(_field.transform);
            _gameField.CreateField(_field.transform);
            _cellSpawner.CreateCells(_field.transform);

            _stateMachine.Enter<GameLoopState>();
        }
    }
}