using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : IEnemyModel
{
    public ReactiveProperty<Transform> Target { get; set; }        
}

