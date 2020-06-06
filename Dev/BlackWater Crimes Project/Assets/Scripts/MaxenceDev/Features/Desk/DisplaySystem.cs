using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySystem : MonoBehaviour
{
    public List<Transform> contents;
    
    private GameObject currentSelected;

    void Start()
    {
        Reset();
    }
    
    void Reset()
    {
        foreach (Transform content in contents)
        {
            if (content.childCount != 0) for (int n = 0; n < content.childCount; n++)
            {
                content.GetChild(n).gameObject.SetActive(false);
            }
        }
    }

    public void DisplayElement(GameObject elementObject, GameObject reportObject)
    {
        Reset();
        
        reportObject.SetActive(true);

        if (elementObject.TryGetComponent<NotificationReport>(out NotificationReport ntr)) elementObject.GetComponent<NotificationReport>().ChangeNotification();

        GameObject select = elementObject.transform.GetChild(0).gameObject;
        SelectElement(select);
    }

    void SelectElement(GameObject element)
    {
        if (currentSelected != null) currentSelected.SetActive(false);

        currentSelected = element;
        currentSelected.SetActive(true);
    }
}
