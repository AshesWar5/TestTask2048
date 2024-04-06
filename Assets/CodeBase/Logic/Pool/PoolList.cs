using System.Collections.Generic;

namespace CodeBase.Logic.Pool
{
    public sealed class PoolList<TObject> : IPoolReturn where TObject : PoolView
    {
        private readonly List<TObject> _inactiveObjects;

        public IReadOnlyList<TObject> InactiveObjects => _inactiveObjects;

        public PoolList()
        {
            _inactiveObjects = new List<TObject>();
        }

        public void Add(TObject obj)
        {
            obj.AssignPool(this);
        }

        public void ReturnToPool(IPoolObject obj)
        {
            obj.Disable();
            _inactiveObjects.Add((TObject)obj);
        }

        public bool HasInactiveObjects()
        {
            return _inactiveObjects.Count > 0;
        }

        public int GetLengthPool()
        {
            return _inactiveObjects.Count;
        }

        public TObject GetInactiveObject(bool removeCompletely = false)
        {
            return GetObject(_inactiveObjects.Count - 1, removeCompletely);
        }

        public TObject GetObject(int index, bool removeCompletely = false)
        {
            if (_inactiveObjects.Count <= index)
                return null;

            var inactiveObject = _inactiveObjects[index];
            inactiveObject.Enable();
            
            if(removeCompletely)
                _inactiveObjects.RemoveAt(index);
            
            return inactiveObject;
        }

        public void Clear()
        {
            foreach (var obj in _inactiveObjects)
            {
                obj.ReleaseFromPool();
            }
            
            _inactiveObjects.Clear();
        }
    }
}