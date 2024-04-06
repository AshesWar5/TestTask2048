using System.Collections.Generic;

namespace CodeBase.Logic.Pool
{
    public sealed class PoolQueue<TObject> : IPoolReturn where TObject : IPoolObject
    {
        private readonly Queue<TObject> _inactiveObjects;

        public PoolQueue()
        {
            _inactiveObjects = new Queue<TObject>();
        }

        public void Add(TObject obj)
        {
            obj.AssignPool(this);
        }

        public void ReturnToPool(IPoolObject obj)
        {
            obj.Disable();
            _inactiveObjects.Enqueue((TObject)obj);
        }

        public bool HasInactiveObjects()
        {
            return _inactiveObjects.Count > 0;
        }

        public int GetLengthPool()
        {
            return _inactiveObjects.Count;
        }

        public TObject GetInactiveObject()
        {
            var inactiveObject = _inactiveObjects.Dequeue();
            inactiveObject.Enable();
            return inactiveObject;
        }
    }
}