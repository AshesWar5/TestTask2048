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

        public override void InstallBindings()
        {
            BindGame();
            BindGameField();
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
    }
}