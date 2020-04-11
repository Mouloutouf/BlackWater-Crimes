using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationSystem : MonoBehaviour
{
    public GameObject notificationObject;

    public Transform elementHolder;
    public int startElement;

    private int reportsNumber;

    private bool start = true;

    public PageSystem pageSystem;
    
    void Update()
    {
        if (start) InitializeNotifications();
        
        if (!pageSystem.Start)
        {
            foreach (Transform tr in elementHolder)
            {
                if (tr.gameObject.activeInHierarchy && !tr.GetComponent<ReportObject>().data.seen)
                {
                    tr.GetComponent<ReportObject>().data.seen = true;

                    ChangeNotifications();
                }
            }
        }
    }

    void InitializeNotifications()
    {
        reportsNumber = elementHolder.childCount - startElement;

        for (int i = startElement; i < elementHolder.childCount; i++)
        {
            if (elementHolder.GetChild(i).GetComponent<ReportObject>() == null) { notificationObject.SetActive(false); start = false; return; }

            if (elementHolder.GetChild(i).GetComponent<ReportObject>().data.seen)
            {
                reportsNumber--;
            }
        }

        if (reportsNumber <= 0) notificationObject.SetActive(false);

        else notificationObject.GetComponentInChildren<Text>().text = reportsNumber.ToString();

        start = false;
    }

    void ChangeNotifications()
    {
        reportsNumber--;

        if (reportsNumber > 0) notificationObject.GetComponentInChildren<Text>().text = reportsNumber.ToString();

        else notificationObject.SetActive(false);
    }
}
