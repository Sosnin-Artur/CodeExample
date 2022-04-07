using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressablesController : MonoBehaviour
{
    [SerializeField]
    private List<AssetReference> _references;
    [SerializeField]
    private Transform _parent;
    private List<GameObject> _createdObjects;

    private async void Start()
    {
       await AddressablesLoader.InitAssets(_references, _parent);
    }
}
