using System;

namespace MVP
{
    public abstract class BasePresenter<T> : IDisposable where T : IView
    {
        private readonly T _view;
        public T View => _view;

        public BasePresenter()
        {
        }

        public BasePresenter(T view)
        {
            _view = view;
        }

        public abstract void Dispose();
    }
}