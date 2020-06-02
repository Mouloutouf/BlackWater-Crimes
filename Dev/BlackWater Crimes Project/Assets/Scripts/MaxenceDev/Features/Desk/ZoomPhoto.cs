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

    public Vector2 imagePosWithText;

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
    
    public void ZoomObject()
    {
        Sprite image = this.gameObject.GetComponent<PhotoObject>().data.photo;
        string nameKey = this.gameObject.GetComponent<PhotoObject>().data.nameKey;

        if (this.gameObject.GetComponent<PhotoObject>().data.hasText)
        {
            string textKey = this.gameObject.GetComponent<PhotoObject>().data.textKey;
            Zoom(image, nameKey, textKey);
        }
        else Zoom(image, nameKey);

        isZoomed = true;
    }

    public void Zoom(Sprite photoSprite, string nameKey)
    {
        zoomPanel.SetActive(true);
        zoomPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photoSprite;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Localisation>().key = nameKey;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Localisation>().RefreshText();

        zoomPanel.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        zoomPanel.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Zoom(Sprite photoSprite, string nameKey, string textKey)
    {
        zoomPanel.SetActive(true);
        zoomPanel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = photoSprite;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Localisation>().key = nameKey;
        zoomPanel.transform.GetChild(0).GetChild(1).GetComponent<Localisation>().RefreshText();

        zoomPanel.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = imagePosWithText;

        zoomPanel.transform.GetChild(1).gameObject.SetActive(true);
        zoomPanel.transform.GetChild(1).GetComponent<Localisation>().key = textKey;
        zoomPanel.transform.GetChild(1).GetComponent<Localisation>().RefreshText();
    }

    public void DeZoom()
    {
        zoomPanel.SetActive(false);
    }

    #region Old
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
    #endregion
}
