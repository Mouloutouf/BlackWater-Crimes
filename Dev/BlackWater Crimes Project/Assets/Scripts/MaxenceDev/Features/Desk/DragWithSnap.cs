using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragWithSnap : MonoBehaviour
{
    public RectTransform imageTransform;

    private float startPosX;
    private float startPosY;

    public bool isHeld;
    public bool isSnapped;

    public bool isUI;
    public bool useWithSnap;
    
    void Update()
    {
        if (isHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            if (!isUI) mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
            if (useWithSnap && !isSnapped) imageTransform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            if (!isUI) mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - transform.localPosition.x;
            startPosY = mousePos.y - transform.localPosition.y;

            isHeld = true;
        }
    }

    void OnMouseUp()
    {
        isHeld = false;
    }

    public void ResetAllPos()
    {
        imageTransform.position = this.GetComponent<RectTransform>().position;
    }
}
