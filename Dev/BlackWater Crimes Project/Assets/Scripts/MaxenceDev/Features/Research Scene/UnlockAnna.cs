using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAnna : MonoBehaviour
{
    public GameData gameData;

    public EvidenceObject evidenceObject;

    public bool photo { get { return evidenceObject.data.unlockedData; } }
    private bool check = true;

    public Locations toUnlock;

    void Update()
    {
        if (photo && check)
        {
            foreach (Location location in gameData.locations)
            {
                if (location.myLocation == toUnlock) location.known = true;
            }

            check = false;
        }
    }
}
