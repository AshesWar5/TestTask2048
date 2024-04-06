using CodeBase.Infrastructures.View;
using TMPro;
using UnityEngine;

namespace CodeBase.Logic
{
    public class TextView : View
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(string text) => _text.text = text;
        
        public void SetColor(Color color) => _text.color = color;
    }
}