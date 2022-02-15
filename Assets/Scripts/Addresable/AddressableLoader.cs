using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
public static class AddressablesLoader 
{
    public static async Task InitAssets(string label, Transform parent)        
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;

        foreach (var location in locations)
        {
            await Addressables.InstantiateAsync(location, parent).Task;
        }

    }
}
