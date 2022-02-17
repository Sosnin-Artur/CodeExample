using System;

namespace MVP
{
    public abstract class BasePresenter<T> : IDisposable where T : IView
    {
        protected T View;
        public BasePresenter(T view)
        {
            View = view;
        }

        public abstract void Dispose();
    }
}