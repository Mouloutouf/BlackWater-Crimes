using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ZoomPhoto : MonoBehaviour
{
    public GameObject zoomPanel;

    [Title("References")]

    public Transform zoomCadran;
    public Image zoomImage;
    public Localisation zoomNameKey;
    public Localisation zoomTextKey;
    
    private bool isZoomed;

    private float timerValue = 0.3f;
    private float timer;

    public Vector2 imagePosWithText;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isZoomed)
        {
            DeZoom();
            isZoomed = false;
        }
    }
    
    public void ZoomObject(GameObject photoObject)
    {
        Sprite image = photoObject.GetComponent<PhotoObject>().data.photo;
        string nameKey = photoObject.GetComponent<PhotoObject>().data.nameKey;

        if (photoObject.GetComponent<PhotoObject>().data.hasText)
        {
            string textKey = photoObject.GetComponent<PhotoObject>().data.textKey;
            Zoom(image, nameKey, textKey);
        }
        else Zoom(image, nameKey);
        
        photoObject.GetComponent<NotificationPhoto>().ChangeNotification();
        
        isZoomed = true;
    }

    public void Zoom(Sprite photoSprite, string nameKey)
    {
        zoomPanel.SetActive(true);

        zoomImage.sprite = photoSprite;
        zoomNameKey.key = nameKey;
        zoomNameKey.RefreshText();

        zoomCadran.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        zoomPanel.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void Zoom(Sprite photoSprite, string nameKey, string textKey)
    {
        zoomPanel.SetActive(true);

        zoomImage.sprite = photoSprite;
        zoomNameKey.key = nameKey;
        zoomNameKey.RefreshText();

        zoomCadran.GetComponent<RectTransform>().anchoredPosition = imagePosWithText;

        zoomTextKey.gameObject.SetActive(true);
        zoomTextKey.key = textKey;
        zoomTextKey.RefreshText();
    }

    public void DeZoom()
    {
        zoomPanel.SetActive(false);
    }
}
