using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ZoomPhoto : MonoBehaviour
{
    [ShowIf("useOld")]
    public Camera cam;
    [ShowIf("useOld")]
    public GameObject zoomPanel;

    private bool isZoomed;

    private float timerValue = 0.3f;
    private float timer;

    public bool useOld;
    
    void Update()
    {
        if (useOld) OldRaycastCheck();

        if (Input.GetMouseButtonDown(0) && isZoomed)
        {
            DeZoom();
            isZoomed = false;
        }
    }

    void OldRaycastCheck()
    {
        if (Input.GetMouseButtonDown(0)) { timer = 0f; isZoomed = false; }
        timer += Time.deltaTime;
        if (timer > timerValue) isZoomed = true;
        else isZoomed = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.parent.GetComponent<PhotoObject>() != null && Input.GetMouseButtonUp(0) && !isZoomed)
        {
            PhotoObject photoScript = hits[0].transform.parent.GetComponent<PhotoObject>();
            Zoom(photoScript.data.photo, photoScript.data.codeName);
            isZoomed = true;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DeZoom();
            isZoomed = false;
        }
    }

    public void ZoomObject()
    {
        Sprite image = this.gameObject.GetComponent<PhotoObject>().data.photo;
        string text = this.gameObject.GetComponent<PhotoObject>().data.codeName;

        Zoom(image, text);

        isZoomed = true;
    }

    public void Zoom(Sprite photoSprite, string photoName)
    {
        zoomPanel.SetActive(true);
        zoomPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photoSprite;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = photoName;
    }

    public void DeZoom()
    {
        zoomPanel.SetActive(false);
    }
}
