using MVP;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

public static class Saver
{    
    public static bool Save(IData model, string savePath)
    {        
        IFormatter formatter = new BinaryFormatter();                
        using (var stream = 
            File.Open(string.Concat(Application.persistentDataPath, savePath), 
            FileMode.OpenOrCreate, FileAccess.Write))
        {                
            formatter.Serialize(stream, model);                
        }        

        return true;
    }    

    public static IData Load(string savePath)
    {
        IData model = null;

        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {            
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);            
            model = (IData)formatter.Deserialize(stream);            
            stream.Close();                        
        }
        
        return model;
    }

    public static async Task<bool> SaveAsync(IData model, string savePath)
    {
        var formatter = new BinaryFormatter();

        var path = string.Concat(Application.persistentDataPath, savePath);

        await Task.Run(() =>
        {
            using (var stream =
                File.Open(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(stream, model);
            }            
        });

        return true;
    }
    
    public static async Task<IData> LoadAsync(string savePath)
    {
        IData data = null;

        var formatter = new BinaryFormatter();
        var path = string.Concat(Application.persistentDataPath, savePath);

        await Task.Run(() =>
        {
            if (File.Exists(path))
            {
                using (var stream = 
                    File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    data = formatter.Deserialize(stream) as IData;
                }
            }            
        });

        return data;
    }
}
