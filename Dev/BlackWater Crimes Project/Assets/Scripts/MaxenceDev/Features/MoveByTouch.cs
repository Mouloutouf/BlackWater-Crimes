using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByTouch : MonoBehaviour
{
    public Camera theCam;

    private bool isHolding;

    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Move(i);
        }

        if (isHolding)
        {
            Move(1);
        }

        /*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
            transform.position = touchPos;
        }
        */
    }

    void OnMouseDown()
    {
        isHolding = true;
    }

    void OnMouseUp()
    {
        isHolding = false;
    }

    void Move(int nik)
    {
        Touch touch = Input.touches[nik];
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
        Debug.DrawLine(Vector3.zero, touchPos, Color.red);
    }
}
