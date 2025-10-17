using MVP;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSystem
{    
    public static int CurrentLevel => SceneManager.GetActiveScene().buildIndex;
    
    public static void LoadLevel(int levelIndex)
    {        
        SceneManager.LoadScene(levelIndex);    
    }    

    public static void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void GoToPreviousLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
