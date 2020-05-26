using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBob : MonoBehaviour
{
    public Locations toUnlock;

    public GameData gameData;

    void Start()
    {
        foreach (Location location in gameData.locations)
        {
            if (location.myLocation == toUnlock) location.known = true;
        }
    }
}
