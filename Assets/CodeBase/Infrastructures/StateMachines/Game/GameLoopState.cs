using CodeBase.Infrastructures.View;
using CodeBase.Logic.Cell;
using CodeBase.Logic.GamePage;

namespace CodeBase.Infrastructures.StateMachines.Game
{
    public sealed class GameLoopState : State<GameStateMachine>, IState
    {
        private readonly ICellMovement _cellMovement;
        private readonly IUIPageFactory _pageFactory;
        private readonly ICellMerge _cellMerge;
        private readonly ICellSpawner _cellSpawner;
        private readonly ICellAnimation _cellAnimation;

        private GamePageView _page;
        
        public GameLoopState(GameStateMachine stateMachine,
            ICellMovement cellMovement, IUIPageFactory pageFactory,
            ICellMerge cellMerge, ICellSpawner cellSpawner,
            ICellAnimation cellAnimation) : base(stateMachine)
        {
            _cellMovement = cellMovement;
            _pageFactory = pageFactory;
            _cellMerge = cellMerge;
            _cellSpawner = cellSpawner;
            _cellAnimation = cellAnimation;
        }

        public void Enter()
        {
            if (_page is null)
            {
                _page = _pageFactory.Create<GamePageView>(PageType.GAME_SCREEN);
                _page.Initialize();
                
                _page.Reload.Click += OnClickReload;
            }

            _page.Show();
            _cellMovement.Initialize();
            OnMerged(0);

            _cellMerge.Merged += OnMerged;
        }

        public void Exit()
        {
            _cellMovement.Stop();
            _cellMerge.Dispose();
            _cellSpawner.Dispose();
            _cellAnimation.Dispose();

            _cellMerge.Merged -= OnMerged;
        }

        public override void Dispose()
        {
            _page.Reload.Click -= OnClickReload;
            
            _page.Dispose();
        }

        private void OnMerged(int score)
        {
            _page.Score.SetText(score.ToString());
        }
        
        private void OnClickReload()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameConstant.GAME_SCENE);
        }
    }
}