using UnityEngine;

public interface ILayerChehkerView
{
    public bool IsInLayerMask(LayerMask mask, GameObject obj);    
}