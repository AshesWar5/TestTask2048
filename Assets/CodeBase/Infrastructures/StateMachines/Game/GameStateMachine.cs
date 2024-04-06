namespace CodeBase.Infrastructures.StateMachines.Game
{
    public sealed class GameStateMachine : StateMachine<GameStateMachine>
    {
        public GameStateMachine(IStateFactory stateFactory) : base(stateFactory) { }
    }
}