using UnityEngine;

namespace CodeBase.Infrastructures.AssetManagement
{
    public interface IAssetProvider
    {
        TType Instantiate<TType>(string path) where TType : MonoBehaviour;
        TType Instantiate<TType>(string path, Vector3 pos) where TType : MonoBehaviour;
        TType Instantiate<TType>(string path, Transform parent, Vector3 pos) where TType : MonoBehaviour;
        void Destroy(View.View view);
    }
}