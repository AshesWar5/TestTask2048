using UnityEngine;

namespace CodeBase.Infrastructures.AssetManagement
{
    public sealed class AssetProvider : IAssetProvider
    {
        public TType Instantiate<TType>(string path) where TType : MonoBehaviour
        {
            return Instantiate<TType>(path, null, Vector3.zero);
        }
        
        public TType Instantiate<TType>(string path, Vector3 pos) where TType : MonoBehaviour
        {
            return Instantiate<TType>(path, null, pos);
        }

        public TType Instantiate<TType>(string path, Transform parent, Vector3 pos) where TType : MonoBehaviour
        {
            var asset = Resources.Load<TType>(path);
            var instance = Object.Instantiate(asset, Vector3.zero, Quaternion.identity, parent);
            instance.transform.localPosition = pos;
            return instance.GetComponent<TType>();
        }

        public void Destroy(View.View view)
        {
            Object.Destroy(view.gameObject);
        }
    }
}