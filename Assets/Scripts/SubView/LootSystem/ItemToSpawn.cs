using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemToSpawn 
{    
    [SerializeField]
    private BaseItemObject _item;
    [SerializeField]
    [Range(0, 1)]
    private float _spawnRate;

    public BaseItemObject Item => _item;
    public float SpawnRate => _spawnRate;
}
