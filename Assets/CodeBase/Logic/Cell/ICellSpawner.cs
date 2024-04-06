using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public interface ICellSpawner : IDisposable
    {
        CellModel[,] Field { get; }
        Dictionary<CellModel, CellView> FieldModelView { get; }
        void CreateCells(Transform parent);
        void GenerateRandomCell();
    }
}