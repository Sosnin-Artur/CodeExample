using UnityEngine;

namespace MVP
{
    public interface IPresenterFactory<V, P> : IFactory<P>, IFactory<object[], P>
        where V : IView
        where P : BasePresenter<V>
    {
        P BindParamsAndCreate(GameObject gameObject, params object[] startedParams);                        

        void BindPresenterParams(GameObject gameObject, params object[] startedParams);

        void UnbindPresenter();

        void UnbindParameters(params object[] startedParams);
    }
}