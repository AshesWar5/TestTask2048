using System;
using CodeBase.Infrastructures.AssetManagement;
using UnityEngine;

namespace CodeBase.Infrastructures.View
{
    public sealed class UIFactory : IUIViewFactory, IUIPageFactory
    {
        private readonly IAssetProvider _asset;

        public UIFactory(IAssetProvider asset)
        {
            _asset = asset;
        }

        TypeView IUIViewFactory.Create<TypeView>(ViewType type, Vector3 pos = new Vector3(),
            Transform parent = null) => CreateView<TypeView>(GetPathView(type), pos, parent);

        TypePage IUIPageFactory.Create<TypePage>(PageType type, Vector3 pos = new Vector3(),
            Transform parent = null) => CreateView<TypePage>(GetPathPage(type), pos, parent);

        private TView CreateView<TView>(string path, Vector3 pos, Transform parent)
            where TView : Infrastructures.View.View
        {
            var instance = _asset.Instantiate<TView>(path, parent, pos);
            return instance;
        }

        public void Destroy(View view)
        {
            view.Dispose();
            _asset.Destroy(view);
        }

        private string GetPathView(ViewType type)
        {
            return type switch
            {
                ViewType.FIELD => AssetPath.FIELD_VIEW,
                ViewType.CELL => AssetPath.CELL_VIEW,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        private string GetPathPage(PageType type)
        {
            return type switch
            {
                PageType.GAME_FIELD => AssetPath.GAME_FIELD_PAGE,
                PageType.GAME_SCREEN => AssetPath.GAME_SCREEN_PAGE,
                PageType.GAME_RESULT_SCREEN => AssetPath.GAME_RESULT_PAGE,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}