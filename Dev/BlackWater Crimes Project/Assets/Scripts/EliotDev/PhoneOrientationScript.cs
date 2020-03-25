using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PhoneOrientationScript : MonoBehaviour
{
    [SerializeField] ScreenOrientation sceneOrientation;
    [SerializeField] bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        TargetFPS();

        SwitchOrientation();
    }

    void TargetFPS()
    {
        if(Application.targetFrameRate != 60)
        {
            Application.targetFrameRate = 60;
        }
    }

    void SwitchOrientation()
    {
        switch (sceneOrientation)
        {
            case ScreenOrientation.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                break;

            case ScreenOrientation.PortraitUpsideDown:
                Screen.orientation = ScreenOrientation.PortraitUpsideDown;
                break;

            case ScreenOrientation.LandscapeLeft:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;

            case ScreenOrientation.LandscapeRight:
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
        }

        if (canRotate)
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToLandscapeRight = true;
        }
        else
        {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToLandscapeRight = false;
        }
    }
}
