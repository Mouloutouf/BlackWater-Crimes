using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Notification : MonoBehaviour
{
    public bool isSeen { get; set; }
    public GameObject notificationObject { get { return this.gameObject; } }

    [Title("Settings")]

    public GameObject nPrefab;
    public Vector2 nPosition = new Vector2(35f, 2f);

    protected GameObject _notification;
    
    public UnityEvent seenEvent;

    protected virtual bool GetNotificationState()
    {
        return true;
    }

    protected GameObject InstantiateNotification(Transform parent)
    {
        GameObject _prefab = Instantiate(nPrefab);
        _prefab.transform.SetParent(parent, false);

        _prefab.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        _prefab.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        _prefab.GetComponent<RectTransform>().anchoredPosition = nPosition;

        return _prefab;
    }

    public virtual void ChangeNotification()
    {
        if (!isSeen)
        {
            _notification.SetActive(false);

            seenEvent.Invoke();
        }
    }
}

public class NotificationPhoto : Notification
{
    void Start()
    {
        if (!GetNotificationState())
        {
            _notification = InstantiateNotification(notificationObject.transform);
        }
    }

    protected override bool GetNotificationState()
    {
        bool state = notificationObject.GetComponent<PhotoObject>().data.seen;

        isSeen = state;

        return state;
    }

    public override void ChangeNotification()
    {
        base.ChangeNotification();

        notificationObject.GetComponent<PhotoObject>().data.seen = false;
    }
}