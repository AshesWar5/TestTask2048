using CodeBase.Logic.Pool;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellView : PoolView
    {
        [SerializeField] private TextView _value;
        [SerializeField] private ImageView _background;

        public void SetValue(string value, Color color)
        {
            _value.SetText(value);
            _value.SetColor(color);
        }

        public void SetColor(Color color) =>
            _background.SetColor(color);
    }
}