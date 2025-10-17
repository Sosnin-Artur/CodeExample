using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public void LoadGame()
    {
        LevelSystem.LoadLevel(1);
    }
}
