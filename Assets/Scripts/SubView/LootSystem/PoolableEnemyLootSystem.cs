using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolableEnemyLootSystem : BaseLootSystem
{
    private GroundItemPool _pool;

    [Inject]
    public void Contruct(GroundItemPool pool)
    {        
        _pool = pool;
    }

    public override void GetItems()
    {
        for (int i = 0, length = Items.Length; i < length; i++)
        {
            var probability = Random.value;
            
            if (probability <= Items[i].SpawnRate)
            {                
                var groundItem = _pool.Get();
                groundItem.Item = Items[i].Item;
            }
        }
    }
}
