using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolableGroundItem : BasePoolableGroundItem
{
    public override void Spawn()
    {
        gameObject.SetActive(true);
    }

    public override void Release()
    {
        gameObject.SetActive(false);
    }    
}
