using MVP;
using System.Collections.Generic;

public abstract class BaseInventoryPresenter : BasePresenter<IInventoryView>
{
    public BaseInventoryPresenter(IInventoryView view) : base(view)
    {

    }

    public abstract void Render(List<BaseInventoryCell> items);
}