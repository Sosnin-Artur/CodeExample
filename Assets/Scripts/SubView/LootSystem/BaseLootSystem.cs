using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLootSystem : MonoBehaviour
{
    [SerializeField]
    protected ItemToSpawn[] Items;    

    public abstract void GetItems();
}
