using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLootSystem : BaseLootSystem
{
    [SerializeField]
    private BaseGroundItem _groundItemPrefab;

    public override void GetItems()
    {
        for (int i = 0, length = Items.Length; i < length; i++)
        {
            var probability = Random.value;
            
            if (probability <= Items[i].SpawnRate)
            {
                var groundItem = Instantiate(_groundItemPrefab, transform.position, Quaternion.identity);
                groundItem.Item = Items[i].Item;
            }
        }
    }
}
