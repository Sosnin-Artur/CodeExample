using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : GenericObjectPool<EnemyView>
{
    public override void AddPoolReference(EnemyView objectToAddReference)
    {
        objectToAddReference.Pool = this;
    }
}
