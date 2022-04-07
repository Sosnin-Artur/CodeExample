using MVP;
public abstract class BaseSpawnerPresenter : BasePresenter<ISpawnerView>
{
    public BaseSpawnerPresenter(ISpawnerView view) : base(view)
    {
    }

    public abstract void CreateEnemy();
}