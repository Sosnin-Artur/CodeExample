namespace ObjectPool
{
    public interface IPoolable
    {
        void Spawn();
        void Release();
    }
}