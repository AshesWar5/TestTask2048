using System;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public interface ICellMerge : IDisposable
    {
        event Action<int> Merged;
        void ResetCellsMerge();
        bool FindCellToMerge(CellModel cell, Vector2 direction);
    }
}