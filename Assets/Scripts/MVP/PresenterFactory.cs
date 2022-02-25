using System;
using UnityEngine;
using Zenject;

namespace MVP
{
    public class PresenterFactory<V, P> : IPresenterFactory<V, P>
        where V : IView
        where P : BasePresenter<V>
    {
        private DiContainer _container;        

        public PresenterFactory(DiContainer container)
        {
            _container = container;            
        }

        public P BindParamsAndCreate(GameObject gameObject, params object[] startedParams)
        {
            BindPresenterParams(gameObject, startedParams);

            return Create();
        }        

        public void BindPresenterParams(GameObject gameObject, params object[] startedParams)
        {
            foreach (var obj in _container.GetDependencyContracts<P>())
            {
                var item = Array.Find<object>(startedParams, o => obj.IsAssignableFrom(o as Type));                
                
                if (item != null)
                {                    
                    if (typeof(V).IsAssignableFrom(item as Type))
                    {                        
                        _container
                            .Bind<V>()
                            .To(item as Type)
                            .FromComponentOn(gameObject)
                            .AsCached();
                    }
                    else if (!_container.HasBinding(obj))
                    {
                        _container
                            .Bind(obj)
                            .To(item as Type)
                            .AsCached();
                    }
                }
            }
        }

        public P Create()
        {
            return _container.Instantiate<P>();
        }        

        public void UnbindPresenter()        
        {
            foreach (var obj in _container.GetDependencyContracts<P>())
            {
                if (_container.HasBinding(obj))
                {                    
                    _container.Unbind(obj);
                }
            }
        }

        public void UnbindParameters(params object[] startedParams)
        {
            foreach (var obj in _container.GetDependencyContracts<P>())
            {
                var item = Array.Find<object>(startedParams, o => obj.IsAssignableFrom(o as Type));

                if (item != null)
                {
                    if (!_container.HasBinding(obj))
                    {
                        _container
                            .Unbind(obj);                            
                    }
                }
            }
        }
    }
}