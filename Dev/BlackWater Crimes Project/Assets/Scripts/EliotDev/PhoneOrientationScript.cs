using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhoneOrientationScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        TargetFPS();

        SetScreen();
    }

    void TargetFPS()
    {
        if(Application.targetFrameRate != 60)
        {
            Application.targetFrameRate = 60;
        }
    }

    void SetScreen()
    {
        if(Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Screen.SetResolution(1480, 720, true);
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
    }
}
