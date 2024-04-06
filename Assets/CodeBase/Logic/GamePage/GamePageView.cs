using UnityEngine;

namespace CodeBase.Logic.GamePage
{
    public class GamePageView : PageView
    {
        [SerializeField] private TextView _score;
        [SerializeField] private ButtonView _reload;

        public TextView Score => _score;
        public ButtonView Reload => _reload;

        public override void Initialize()
        {
            Reload.Initialize();
        }

        public override void Dispose()
        {
            Reload.Dispose();
        }
    }
}