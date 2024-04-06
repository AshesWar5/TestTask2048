using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructures.StateMachines
{
    public abstract class StateMachine<FSM> : IStateMachine<FSM>
        where FSM : class, IStateMachine<FSM>
    {
        private readonly IStateFactory _stateFactory;
        protected Dictionary<Type, State<FSM>> _states;
        private IExitableState _activeState;

        protected StateMachine(IStateFactory stateFactory)
        {
            _states = new Dictionary<Type, State<FSM>>();
            _stateFactory = stateFactory;
        }

        public void Enter<TState>() where TState : State<FSM>, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload)
            where TState : State<FSM>, IPayLoadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void Dispose()
        {
            foreach (var state in _states.Values)
            {
                state?.Dispose();
            }
        }

        private TState ChangeState<TState>() where TState : State<FSM>, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : State<FSM>, IExitableState
        {
            if (_states.ContainsKey(typeof(TState)))
            {
                return _states[typeof(TState)] as TState;
            }

            var state = _stateFactory.CreateState<TState>();
            _states.Add(typeof(TState), state);
            return state;
        }
    }
}