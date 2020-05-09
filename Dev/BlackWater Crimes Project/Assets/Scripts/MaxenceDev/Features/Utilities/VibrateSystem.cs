using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrateSystem : MonoBehaviour
{
    public GameData gameData;
    
    public void PhoneVibrate()
    {
        if (gameData.vibrations) Handheld.Vibrate();
    }
}
