using CodeBase.Infrastructures.StateMachines.Game;
using Zenject;
using UnityEngine;

namespace CodeBase.Infrastructures
{
    public class Bootstrap : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Awake()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}