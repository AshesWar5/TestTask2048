using System;

namespace CodeBase.Infrastructures.StateMachines
{
    public abstract class State<TFsm> : IDisposable where TFsm : IStateMachine<TFsm>
    {
        protected readonly TFsm _stateMachine;

        protected State(TFsm stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Dispose() { }
    }
}