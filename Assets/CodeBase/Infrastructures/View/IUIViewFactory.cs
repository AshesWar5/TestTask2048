using UnityEngine;

namespace CodeBase.Infrastructures.View
{
    public interface IUIViewFactory
    {
        TypeView Create<TypeView>(ViewType type,
            Vector3 pos = new Vector3(), Transform parent = null) where TypeView : View;

        void Destroy(View view);
    }
}