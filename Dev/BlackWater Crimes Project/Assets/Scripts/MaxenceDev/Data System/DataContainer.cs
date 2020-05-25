using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public GameData gameData;

    public void EraseCurrentGame()
    {
        gameData.ResetData();
    }

    void Start()
    {
        gameData.ManageData(Action.Save);
    }

    void OnApplicationQuit()
    {
        gameData.ManageData(Action.Save);
    }
}
