using Zenject;
using ObjectPool;

public abstract class BasePoolableEnemyPresenter : 
    BaseEnemyPresenter, ObjectPool.IPoolable
{
    protected LazyInject<GenericObjectPool<
                    BasePoolableEnemyPresenter,
                    IEnemyFactory<BasePoolableEnemyPresenter>>> Pool;

    public BasePoolableEnemyPresenter(
        IEnemyView view, 
        BaseEnemyStateMachine stateMachine, 
        LazyInject<GenericObjectPool<
                    BasePoolableEnemyPresenter,
                    IEnemyFactory<BasePoolableEnemyPresenter>>> pool) : base(view, stateMachine)
    {
        Pool = pool;
    }

    public abstract void Spawn();
    public abstract void Release();        
}