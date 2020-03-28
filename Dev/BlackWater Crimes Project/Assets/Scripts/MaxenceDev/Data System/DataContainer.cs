using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
    public GameData gameData;

    void Update()
    {
        foreach (bool b in gameData.dataListsContainingState.Values)
        {
            //if (!b) b = true;
        }
    }
}
