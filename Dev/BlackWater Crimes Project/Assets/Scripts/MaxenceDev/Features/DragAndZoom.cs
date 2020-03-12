using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndZoom : MonoBehaviour
{
    private Vector3 startTouchPos;

    public Camera cam;

    public float factor;

    //public float smoothing;

    // Zoom Variables
    private float minZoomOut = 1;
    private float maxZoomIn = 8;

    private Vector2 localCamPosition;

    private Vector2 horizontalClamp = new Vector2(-5f, 5f);
    private Vector2 verticalClamp = new Vector2(-5f, 5f);
    
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Input.mousePosition;
            startTouchPos = cam.WorldToScreenPoint(new Vector3(vec.x, vec.y, -10));
        }

        //PinchToZoom();

        if (Input.GetMouseButton(0))
        {
            Vector3 vec = Input.mousePosition;
            Vector3 camDir = startTouchPos - cam.WorldToScreenPoint(new Vector3(vec.x, vec.y, -10));
            Vector3 smoothDir = camDir * factor;
            cam.transform.position = new Vector3(
                Mathf.Clamp(cam.transform.position.x + smoothDir.x, horizontalClamp.x, horizontalClamp.y),
                Mathf.Clamp(cam.transform.position.y + smoothDir.y, verticalClamp.x, verticalClamp.y),
                -10);
        }
        */
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) Debug.Log("ya de l'espoir"); ZoomOnQuarter(transform);
    }

    void PinchToZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            Vector3 prevTouchOnePos = touchOne.position - touchOne.deltaPosition;
            Vector3 prevTouchTwoPos = touchTwo.position - touchTwo.deltaPosition;

            float prevDistance = (prevTouchOnePos - prevTouchTwoPos).magnitude;
            float currentDistance = (touchOne.position - touchTwo.position).magnitude;

            float difference = currentDistance - prevDistance;

            Zoom(difference * 0.01f);
        }
    }

    void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, minZoomOut, maxZoomIn);
    }

    Vector3 ConvertIntoUnityDialect(Vector3 vector)
    {
        Vector3 newVector = Camera.main.ScreenToWorldPoint(new Vector3(vector.x, vector.y, 10));
        return newVector;
    }

    public void ZoomOnQuarter(Transform tr)
    {
        cam.transform.position = new Vector3(tr.position.x, tr.position.y, -10);
        localCamPosition = tr.position;

        cam.orthographicSize = 2;

        factor *= 0.01f;

        horizontalClamp = new Vector2(localCamPosition.x - 2f, localCamPosition.x + 2f);
        verticalClamp = new Vector2(localCamPosition.y - 1.5f, localCamPosition.y + 1.5f);
    }

    public void DezoomToFullMap()
    {
        cam.transform.position = Vector2.zero;
        
        cam.orthographicSize = 5;

        factor *= 100f;

        horizontalClamp = new Vector2(-5f, 5f);
        verticalClamp = new Vector2(-5f, 5f);
    }
}
