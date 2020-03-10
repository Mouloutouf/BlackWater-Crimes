using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //Debug.Log(touch.position);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            touchPos.z = 0;
            transform.position = touchPos;
        }
    }
}
