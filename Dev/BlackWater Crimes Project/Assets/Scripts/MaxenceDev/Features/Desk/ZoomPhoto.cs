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
    
    public Vector2 imagePosWithText;

    private bool isZoomed;

    public float timerValue = 0.3f;
    private float timer;

    private GameObject zoomedObject;
    private int input = 0;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isZoomed)
        {
            Dezoom();
            isZoomed = false;
        }

        if (zoomedObject != null)
        {
            WaitForZoom(zoomedObject);
        }
    }
    
    public void ZoomObject(GameObject photoObject)
    {
        if (input == 0)
        {
            zoomedObject = photoObject;
            input = 1;

            timer = timerValue;
        }
        else
        {
            if (photoObject == zoomedObject)
            {
                input = 2;
            }
            else
            {
                zoomedObject = photoObject;
                input = 1;

                timer = timerValue;
            }
        }
    }

    public void WaitForZoom(GameObject photoObject)
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0.0f)
        {
            zoomedObject = null;
            input = 0;
        }
        else
        {
            if (input == 2)
            {
                Zoom(photoObject);

                zoomedObject = null;
                input = 0;
            }
        }
    }

    public void Zoom(GameObject photoObject)
    {
        zoomPanel.SetActive(true);

        zoomImage.sprite = photoObject.GetComponent<PhotoObject>().data.photo;

        zoomNameKey.key = photoObject.GetComponent<PhotoObject>().data.nameKey;
        zoomNameKey.RefreshText();

        if (photoObject.GetComponent<PhotoObject>().data.hasText)
        {
            zoomCadran.GetComponent<RectTransform>().anchoredPosition = imagePosWithText;

            zoomTextKey.gameObject.SetActive(true);

            zoomTextKey.key = photoObject.GetComponent<PhotoObject>().data.textKey;
            zoomTextKey.RefreshText();
        }
        else
        {
            zoomCadran.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            zoomTextKey.gameObject.SetActive(false);
        }
        
        photoObject.GetComponent<NotificationPhoto>().ChangeNotification();
        
        isZoomed = true;
    }
    
    public void Dezoom()
    {
        zoomPanel.SetActive(false);
    }
}
