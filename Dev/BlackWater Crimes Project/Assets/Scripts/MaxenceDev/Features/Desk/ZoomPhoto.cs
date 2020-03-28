using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ZoomPhoto : MonoBehaviour
{
    public Camera cam;
    public GameObject zoomPanel;

    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(cam.ScreenToWorldPoint(Input.mousePosition), new Vector3(0, 0, 1));

        if (hits.Count() > 0 && hits[0].transform.parent.GetComponent<PhotoObject>() != null && Input.GetMouseButtonDown(0))
        {
            PhotoObject photoScript = hits[0].transform.parent.GetComponent<PhotoObject>();
            Zoom(photoScript.data.photo, photoScript.data.name);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            DeZoom();
        }
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
