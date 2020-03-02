using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardPhone : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    public Text notes;

    // Start is called before the first frame update
    public void OpenKeyboard()
    {
        Debug.Log("test");
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    public void ChangeOrientation()
    {
        if(Screen.orientation == ScreenOrientation.Landscape)
        {
            Screen.autorotateToLandscapeLeft = false;
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if(Screen.orientation == ScreenOrientation.Portrait)
        {
            Screen.autorotateToPortrait = false;
            Screen.orientation = ScreenOrientation.Landscape;
        }
        else if(Screen.orientation == ScreenOrientation.AutoRotation)
        {
            Screen.autorotateToPortrait = false;
            Screen.orientation = ScreenOrientation.Landscape;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                notes.text = keyboard.text;
                keyboard = null;
            }
        }
    }
}
