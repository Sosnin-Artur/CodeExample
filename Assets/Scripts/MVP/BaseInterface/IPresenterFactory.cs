using UnityEngine;

namespace MVP
{
    public interface IPresenterFactory<V, P> : IFactory<P>
        where V : IView
        where P : BasePresenter<V>
    {
        public P BindParamsAndCreate(GameObject gameObject, params object[] startedParams);                        

        public void BindPresenterParams(GameObject gameObject, params object[] startedParams);        

        public void UnbindPresenter();

        public void UnbindParameters(params object[] startedParams);
    }
}