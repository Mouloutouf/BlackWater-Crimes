using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[Serializable]
public class Notification
{
    public Transform content;
    [HideIf("photo")]
    public GameObject display;

    public bool photo;

    public int elements;

    public List<Transform> subContents { get; set; } = new List<Transform>();
}

public class NotificationSystem : MonoBehaviour
{
    public List<Notification> notifications = new List<Notification>();
    public GameObject photoDisplay;
    private int photoElements;

    public GameObject newPrefab;
    public Vector2 prefabPos;

    public bool useOld;
    [ShowIf("useOld")]
    public int startElement = 0;
    
    void Start()
    {
        foreach (Notification notification in notifications)
        {
            GetNotifications(notification);

            if (!notification.photo)
            {
                if (notification.elements <= 0) notification.display.SetActive(false); // If the Elements are all already seen, disables the Count Notification

                else notification.display.GetComponentInChildren<Text>().text = notification.elements.ToString(); // Else sets the Count Notification to the number of unseen Elements
            }
            else
            {
                photoElements += notification.elements; // For Photos, adds the local content Count to the Main Count
            }
        }

        if (photoElements <= 0) photoDisplay.SetActive(false);
        else photoDisplay.GetComponentInChildren<Text>().text = photoElements.ToString();
    }

    void Update()
    {
        foreach (Notification notification in notifications)
        {
            foreach (Transform cont in notification.subContents)
            {
                foreach (Transform tr in cont)
                {
                    if (tr.gameObject.activeInHierarchy)
                    {
                        if (tr.GetComponent<ReportObject>() != null && !tr.GetComponent<ReportObject>().data.seen) // Check for Reports
                        {
                            tr.GetComponent<ReportObject>().data.seen = true;

                            //tr.GetComponent<ReportObject>().SetQuestion();

                            ChangeNotifications(notification);

                            tr.GetComponent<ElementHolder>().bind.transform.GetChild(3).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void See(GameObject obj)
    {
        if (obj.GetComponent<PhotoObject>() != null && !obj.GetComponent<PhotoObject>().data.seen)
        {
            obj.GetComponent<PhotoObject>().data.seen = true;

            ChangeNotifications();

            obj.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    void GetNotifications(Notification current)
    {
        if (current.photo)
        {
            foreach (Transform tr in current.content)
            {
                current.subContents.Add(tr);
            }
        }
        else current.subContents.Add(current.content);

        foreach (Transform cont in current.subContents)
        {
            current.elements += cont.childCount - startElement; // Calculates the number of Elements depending on the number of children

            for (int i = startElement; i < cont.childCount; i++) // Checks the validity of each child, if seen : removes it from the Elements, if not : instantiate a New Notification to it
            {
                if (current.photo) // Check for Photos
                {
                    GameObject localObj = cont.GetChild(i).gameObject;
                    cont.GetChild(i).GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { See(localObj); });

                    if (cont.GetChild(i).GetComponent<PhotoObject>().data.seen) current.elements--;
                    else InstantiateNew(cont.GetChild(i));
                }
                else // Check for Reports
                {
                    if (cont.GetChild(i).GetComponent<ReportObject>().data.seen) current.elements--;
                    else InstantiateNew(cont.GetChild(i));
                }
            }
        }
    }
    
    void ChangeNotifications(Notification current)
    {
        current.elements--;

        if (current.elements > 0) current.display.GetComponentInChildren<Text>().text = current.elements.ToString();

        else current.display.SetActive(false);
    }

    void ChangeNotifications()
    {
        photoElements--;

        if (photoElements > 0) photoDisplay.GetComponentInChildren<Text>().text = photoElements.ToString();

        else photoDisplay.SetActive(false);
    }

    void InstantiateNew(Transform tr)
    {
        GameObject _prefab = Instantiate(newPrefab);

        GameObject parent = tr.GetComponent<ElementHolder>().bind;
        _prefab.transform.SetParent(parent.transform, false);

        _prefab.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        _prefab.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        _prefab.GetComponent<RectTransform>().anchoredPosition = prefabPos;
    }
}
