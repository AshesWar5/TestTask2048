using System;

namespace CodeBase.Logic.Cell
{
    public class CellModel
    {
        public event Action ChangeModel;
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int Point => IsEmpty ? 0 : (int) Math.Pow(2, Value);
        public bool IsEmpty => Value == 0;
        public bool HasMerged { get; private set; }

        public void SetValue(int x, int y, int value, bool reportChange = true)
        {
            X = x;
            Y = y;
            Value = value;
            
            if(reportChange)
                ChangeModel?.Invoke();
        }

        public void IncreaseValue()
        {
            Value++;
            HasMerged = true;
        }

        public void ResetMerged()
        {
            HasMerged = false;
        }
    }
}