using CodeBase.Logic.Animation_UI;
using CodeBase.Logic.Field;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructures.Installers
{
    [CreateAssetMenu(fileName = "Global Installer", menuName = "Installers/Global")]
    public sealed class GlobalScriptableObjectInstaller : ScriptableObjectInstaller<GlobalScriptableObjectInstaller>
    {
        [SerializeField] private GameConfig _game;
        [SerializeField] private GameFieldConfig _gameField;
        [SerializeField] private AnimationUIConfig _animationUI;

        public override void InstallBindings()
        {
            BindGame();
            BindGameField();
            BindAnimationUI();
        }
        
        private void BindGame()
        {
            Container
                .Bind<GameConfig>()
                .FromInstance(_game)
                .AsSingle();
        }
        
        private void BindGameField()
        {
            Container
                .Bind<GameFieldConfig>()
                .FromInstance(_gameField)
                .AsSingle();
        }
        
        private void BindAnimationUI()
        {
            Container
                .Bind<AnimationUIConfig>()
                .FromInstance(_animationUI)
                .AsSingle();
        }
    }
}