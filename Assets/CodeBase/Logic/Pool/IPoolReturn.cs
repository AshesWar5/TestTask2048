namespace CodeBase.Logic.Pool
{
    public interface IPoolReturn
    {
        void ReturnToPool(IPoolObject obj);
    }
}