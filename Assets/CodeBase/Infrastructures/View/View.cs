using System;
using UnityEngine;

namespace CodeBase.Infrastructures.View
{
    public class View : MonoBehaviour, IInitialize, IDisposable
    {
        public RectTransform Rect { get; private set; }

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Initialize() { }

        public virtual void Dispose() { }
    }
}