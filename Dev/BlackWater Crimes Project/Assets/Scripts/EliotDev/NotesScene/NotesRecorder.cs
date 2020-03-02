using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotesRecorder : MonoBehaviour
{

    [SerializeField] Text notes;

    TouchScreenKeyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void OpenKeyboard()
    {
        if (keyboard == null)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && keyboard != null)
        {
            if (keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                if(notes.text == "Your notes...")
                {
                    notes.text = "";
                }
                if (notes.text == "")
                {
                    notes.text += keyboard.text;
                }
                else
                {
                    notes.text += "\n" + keyboard.text;
                }
                keyboard = null;
            }
        }
    }

    public void QuitScene()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.UnloadSceneAsync("NotesSceneProto");
    }
}
