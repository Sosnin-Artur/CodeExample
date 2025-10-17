using UnityEngine;

public interface ILayerChehkerView
{
    bool IsInLayerMask(LayerMask mask, GameObject obj);    
}