using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace MVP
{
    public class MvpInstaller : MonoInstaller
    {                
        protected void CreateMvp<V, P>(V view, params object[] par)                         
            where V : IView
            where P : BasePresenter<V>      
        {
            Container.Instantiate<P>(GetParamsFor<P>(view, par));            
            
        }        

        private IEnumerable<object> GetParamsFor<P>(IView view, params object[] startedParams)
        {            
            var res = new List<object>(startedParams);
            
            foreach (var obj in Container.GetDependencyContracts<P>())
            {
                var item = Array.Find<object>(startedParams, o => obj.IsAssignableFrom(o as Type));
                if (item != null)
                {
                    if (!Container.HasBinding(obj))
                    {                                           
                        Container.Bind(obj).To(item as Type).AsSingle();                        
                    }                    
                    res.Remove(item);                    
                }                    
            }            
            
            res.Add(view);                       
            return res;
        }
    }    
}


