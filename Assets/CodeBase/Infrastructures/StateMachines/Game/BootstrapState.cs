namespace CodeBase.Infrastructures.StateMachines.Game
{
    public sealed class BootstrapState : State<GameStateMachine>, IState
    {
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(SceneLoader sceneLoader,
            GameStateMachine stateMachine) : base(stateMachine)
        {
            _sceneLoader = sceneLoader; 
        }

        public void Enter() =>
            _sceneLoader.Load(GameConstant.INITIAL_SCENE, OnEnterLoadLevel);

        public void Exit() { }

        private void OnEnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>(GameConstant.GAME_SCENE);
    }
}