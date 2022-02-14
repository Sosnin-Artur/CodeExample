using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressablesController : MonoBehaviour
{
    [SerializeField]
    private string _label;
    [SerializeField]
    private Transform _parent;
    private List<GameObject> _createdObjects;

    private async void Start()
    {
       await AddressablesLoader.InitAssets(_label, _parent);
    }
}
