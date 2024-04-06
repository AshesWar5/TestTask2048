using UnityEngine;

namespace CodeBase.Logic.Field
{
    [CreateAssetMenu(fileName = "Game Field Config", menuName = "Game Field")]
    public sealed class GameFieldConfig : ScriptableObject
    {
        [SerializeField] private float _cellSize;
        [SerializeField] private float _spacing;
        [SerializeField] private int _fieldSize;
        [SerializeField] private int _startCountSpawnCell;
        [SerializeField] private Color[] _colorsCells;
        [SerializeField] private Color _cellDarkColor;
        [SerializeField] private Color _cellLightColor;
        [SerializeField] private float _timeMoveCell;

        public float CellSize => _cellSize;
        public float Spacing => _spacing;
        public int FieldSize => _fieldSize;
        public int StartCountSpawnCell => _startCountSpawnCell;
        public Color[] ColorsCells => _colorsCells;
        public Color CellDarkColor => _cellDarkColor;
        public Color CellLightColor => _cellLightColor;
        public float TimeMoveCell => _timeMoveCell;
    }
}