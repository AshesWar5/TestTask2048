using CodeBase.Infrastructures.View;

namespace CodeBase.Logic.Pool
{
    public abstract class PoolView : View, IPoolObject
    {
        private IPoolReturn _pool;

        public void AssignPool(IPoolReturn pool)
        {
            _pool = pool;
        }

        public void Enable()
        {
            Show();
        }

        public void Disable()
        {
            Hide();
        }

        public void ReturnToPool()
        {
            _pool.ReturnToPool(this);
        }

        public void ReleaseFromPool()
        {
            _pool = null;
        }
    }
}