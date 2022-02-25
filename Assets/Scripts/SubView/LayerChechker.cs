using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LayerChechker : MonoBehaviour, ILayerChehkerView
{                
    public bool IsInLayerMask(LayerMask mask, GameObject obj)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }    
}
