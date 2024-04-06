using System;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public interface ICellAnimation : IDisposable
    {
        void SetParentView(Transform parent);
        void SmoothTransition(CellModel from, CellModel to, bool isMerging);
    }
}