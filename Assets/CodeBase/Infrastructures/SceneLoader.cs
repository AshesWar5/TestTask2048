using System;
using System.Collections;
using CodeBase.Infrastructures.StateMachines.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructures
{
    public sealed class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStateMachine _stateMachine;

        public SceneLoader(ICoroutineRunner coroutineRunner, GameStateMachine stateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine;
        }

        public void Load(string name, Action onLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            _stateMachine.Dispose();
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
            while (!waitNextScene.isDone)
                yield return null;
            onLoaded?.Invoke();
        }
    }
}