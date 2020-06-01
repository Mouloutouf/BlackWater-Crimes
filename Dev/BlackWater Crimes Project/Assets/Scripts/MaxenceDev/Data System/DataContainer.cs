using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public bool menu;
    
    public GameData gameData;

    public void EraseCurrentGame()
    {
        gameData.ResetData();
    }
}
