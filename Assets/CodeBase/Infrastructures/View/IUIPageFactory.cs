using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructures.View
{
    public interface IUIPageFactory
    {
        TypePage Create<TypePage>(PageType type, 
            Vector3 pos = new Vector3(), Transform parent = null) where TypePage : PageView;
    }
}