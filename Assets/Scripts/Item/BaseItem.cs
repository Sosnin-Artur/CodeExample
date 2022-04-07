using UnityEngine;

public class BaseItem : MonoBehaviour
{
    [SerializeField]
    private BaseItemObject _item;

    public virtual BaseItemObject Item 
    {
        get => _item;
        set => _item = value;
    }
}