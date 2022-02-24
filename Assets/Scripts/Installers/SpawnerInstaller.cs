using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using MVP;

public class SpawnerInstaller : MvpInstaller
{
    [SerializeField]
    private SpawnerView _view;

    public override void InstallBindings()
    {        
        CreateMvp<ISpawnerView, SpawnerPresenter>(_view, typeof(EnemyPool));
    }    
}