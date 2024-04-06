using CodeBase.Infrastructures.View;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Logic
{
    public class ImageView : View
    {
        [SerializeField] private Image _image;

        public void SetImage(Sprite sprite)
            => _image.sprite = sprite;

        public void SetColor(Color color)
            => _image.color = color;

        public void SetAmount(float amount)
            => _image.fillAmount = amount;
    }
}