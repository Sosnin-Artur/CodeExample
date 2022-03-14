using MVP;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Saver
{    
    public static void Save(IData model, string savePath)
    {        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, model);
        stream.Close();
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
}
