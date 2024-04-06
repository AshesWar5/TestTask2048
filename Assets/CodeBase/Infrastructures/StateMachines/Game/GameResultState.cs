using CodeBase.Infrastructures.View;
using CodeBase.Logic.GameResultScreen;

namespace CodeBase.Infrastructures.StateMachines.Game
{
    public sealed class GameResultState : State<GameStateMachine>, IPayLoadedState<bool>
    {
        private readonly IUIPageFactory _pageFactory;

        private GameResultScreenPageView _page;
        
        public GameResultState(IUIPageFactory pageFactory, GameStateMachine stateMachine) :
            base(stateMachine)
        {
            _pageFactory = pageFactory;
        }

        public void Enter(bool win)
        {
            if (_page is null)
            {
                _page = _pageFactory.Create<GameResultScreenPageView>(PageType.GAME_RESULT_SCREEN);
            }
            
            _page.Result.SetText(win ? "WIN" : "LOSE");
            _page.Show();
        }

        public void Exit()
        {
            _page.Hide();
        }
    }
}