using System;
using CodeBase.Infrastructures.View;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class ButtonView : View
    {
        public event Action Click;
        
        [SerializeField] private Button _button;

        public override void Initialize()
        {
            _button.onClick.AddListener(OnClick);
        }

        public override void Dispose()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetColor(Color color)
        {
            _button.targetGraphic.color = color;
        }

        private void OnClick()
        {
            Click?.Invoke();
        }
    }
}