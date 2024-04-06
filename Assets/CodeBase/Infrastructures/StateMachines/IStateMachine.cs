using System;

namespace CodeBase.Infrastructures.StateMachines
{
    public interface IStateMachine<FSM> : IDisposable where FSM : IStateMachine<FSM>
    {
        void Enter<TState>() where TState : State<FSM>, IState;

        void Enter<TState, TPayload>(TPayload payload)
            where TState : State<FSM>, IPayLoadedState<TPayload>;
    }
}