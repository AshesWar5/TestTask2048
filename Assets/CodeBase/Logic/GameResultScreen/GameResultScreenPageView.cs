using UnityEngine;

namespace CodeBase.Logic.GameResultScreen
{
    public sealed class GameResultScreenPageView : PageView
    {
        [SerializeField] private TextView _result;

        public TextView Result => _result;
    }
}