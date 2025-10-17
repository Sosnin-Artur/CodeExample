using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : IPlayerModel
{                
    public ReactiveProperty<Vector3> Position { get; set; }    
}
