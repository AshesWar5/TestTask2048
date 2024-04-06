namespace CodeBase.Infrastructures.StateMachines
{
    public interface IPayLoadedState<TPayload> : IExitableState
    {
        void Enter(TPayload value);
    }
}