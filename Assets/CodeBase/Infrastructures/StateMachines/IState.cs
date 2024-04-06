namespace CodeBase.Infrastructures.StateMachines
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}