using CodeBase.Infrastructures.AssetManagement;
using CodeBase.Infrastructures.Controller;
using CodeBase.Infrastructures.StateMachines.Game;
using CodeBase.Infrastructures.View;
using CodeBase.Logic.Cell;
using CodeBase.Logic.Field;
using CodeBase.Services.Input_Game;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructures.Installers
{
    public class GlobalInstaller : MonoInstaller, ICoroutineRunner
    { 
        public override void InstallBindings()
        {
            BindCoroutineRunner();
            BindLoaderScene();
            BindAssetProvider();
            BindUIFactory();
            BindStateMachineFactory();
            BindControllerFactory();
            BindInput();
            BindGameField();
            BindCellAnimation();
            BindCellSpawner();
            BindCellMovement();
            BindCellMerge();
            BindCellGameResult();
            BindGameControllers();
            BindGameStateMachine();
        }

        private void BindCoroutineRunner()
        {
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindLoaderScene()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }

        private void BindAssetProvider()
        {
            Container
                .Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }

        private void BindUIFactory()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
        }

        private void BindStateMachineFactory()
        {
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();
        }

        private void BindControllerFactory()
        {
            Container
                .Bind<IControllerFactory>()
                .To<ControllerFactory>()
                .AsSingle();
        }
        
        private void BindGameControllers()
        {
            Container.Bind<CellController>().AsTransient();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<GameResultState>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }

        private void BindGameField()
        {
            Container
                .Bind<IGameField>()
                .To<GameField>()
                .AsSingle();
        }
        
        private void BindCellSpawner()
        {
            Container
                .Bind<ICellSpawner>()
                .To<CellSpawner>()
                .AsSingle();
        }
        
        private void BindCellMovement()
        {
            Container
                .Bind<ICellMovement>()
                .To<CellMovement>()
                .AsSingle();
        }
        
        private void BindCellMerge()
        {
            Container
                .Bind<ICellMerge>()
                .To<CellMerge>()
                .AsSingle();
        }
        
        private void BindCellAnimation()
        {
            Container
                .Bind<ICellAnimation>()
                .To<CellAnimation>()
                .AsSingle();
        }
        
        private void BindCellGameResult()
        {
            Container
                .Bind<ICellGameResult>()
                .To<CellGameResult>()
                .AsSingle();
        }

        private void BindInput()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container
                    .BindInterfacesAndSelfTo<MobileInput>()
                    .AsSingle();
            }
            else
            {
                Container
                    .BindInterfacesAndSelfTo<StandaloneInput>()
                    .AsSingle();
            }
        }
    }
}