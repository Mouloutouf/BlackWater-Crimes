using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskNotification : MonoBehaviour
{
    public GameData gameData;

    public GameObject notification;

    void Start()
    {
        if (notification == null) return;
        
        if (gameData.newStuff) notification.SetActive(true);
        else notification.SetActive(false);
    }

    public void ResetStuff()
    {
        gameData.newStuff = false;
    }
}
