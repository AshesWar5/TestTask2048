using UnityEngine;

namespace CodeBase.Infrastructures
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField] private int _maxValueCell = 10;

        public int MaxValueCell => _maxValueCell;
    }
}