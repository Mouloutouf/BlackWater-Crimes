using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySystem : MonoBehaviour
{
    public Transform content;

    public int startIndex;
    
    private GameObject currentSelected;

    void Start()
    {
        if (content.childCount != 0) for (int n = startIndex; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
    }
    
    public void DisplayElement(GameObject elementObject, GameObject reportObject)
    {
        for (int n = startIndex; n < content.childCount; n++) content.GetChild(n).gameObject.SetActive(false);
        
        reportObject.SetActive(true);

        if (elementObject.TryGetComponent<NotificationReport>(out NotificationReport ntr)) elementObject.GetComponent<NotificationReport>().ChangeNotification();
    }

    public void SelectElement(GameObject element)
    {
        if (currentSelected != null) currentSelected.SetActive(false);

        currentSelected = element;
        currentSelected.SetActive(true);
    }
}
