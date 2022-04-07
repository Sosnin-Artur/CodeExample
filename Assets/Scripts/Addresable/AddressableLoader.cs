using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public static class AddressablesLoader 
{
    public static async Task InitAssets(List<AssetReference> references, Transform parent)        
    {        
        foreach (var reference in references)
        {
            await Addressables.InstantiateAsync(reference, parent).Task;
        }
    }
}
