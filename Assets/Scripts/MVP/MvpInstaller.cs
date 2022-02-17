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
        protected void CreateMvp<M, V, P>(V view)             
            where M : class, IModel 
            where V : IView
            where P : BasePresenter<V>      
        {
            Container.BindInterfacesTo<M>().AsSingle();
            Container.Bind<V>().FromInstance(view);            
            Container.BindInterfacesAndSelfTo<P>().AsSingle();                       
        }        
    }
}


