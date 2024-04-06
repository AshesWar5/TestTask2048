using CodeBase.Infrastructures.StateMachines;

namespace CodeBase.Infrastructures
{
    public interface IStateFactory
    {
        T CreateState<T>() where T : IExitableState;
    }
}