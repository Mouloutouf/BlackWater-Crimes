﻿using System.Collections;
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

    void Start()
    {
        if (!menu) gameData.ManageData(Action.Save);
    }

    void OnApplicationPause()
    {
        gameData.firstTimeInMenu = true;

        gameData.ManageData(Action.Save);
    }
}
