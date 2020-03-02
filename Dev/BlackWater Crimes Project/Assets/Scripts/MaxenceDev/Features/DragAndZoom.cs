using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndZoom : MonoBehaviour
{
    private Vector3 startTouchPos;

    public Camera cam;

    public float factor;

    public float smoothing;

    private float minZoomOut = 1;
    private float maxZoomIn = 8;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vec = Input.mousePosition;
            startTouchPos = cam.WorldToScreenPoint(new Vector3(vec.x, vec.y, -10));
        }

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

        else if (Input.GetMouseButton(0))
        {
            Vector3 vec = Input.mousePosition;
            Vector3 camDir = startTouchPos - cam.WorldToScreenPoint(new Vector3(vec.x, vec.y, -10));
            Vector3 smoothDir = camDir * 0.000007f;
            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x + smoothDir.x, -5, 5), Mathf.Clamp(cam.transform.position.y + smoothDir.y, -5, 5), -10);
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
}
