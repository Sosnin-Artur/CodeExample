abstract public class BasePresenter<T> where T : IView
{
    protected T View;
    public BasePresenter(T view)
    {
        View = view;        
    }

}