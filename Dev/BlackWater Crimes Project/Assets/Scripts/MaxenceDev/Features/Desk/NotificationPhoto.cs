﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Notification : MonoBehaviour
{
    public NotificationSystem notificationSystem;

    [HideInInspector] public bool isSeen;
    public GameObject notificationObject { get { return this.gameObject; } }

    [Title("Settings")]

    public GameObject nPrefab;
    public Vector2 nPosition = new Vector2(35f, 2f);

    protected GameObject notificationNew;
    
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
            notificationNew.SetActive(false);

            seenEvent.Invoke();
        }
    }
}

public class NotificationPhoto : Notification
{
    public void Start()
    {
        isSeen = GetNotificationState();

        if (!isSeen) notificationNew = InstantiateNotification(notificationObject.transform);

        SetNotificationSystem();
    }

    protected override bool GetNotificationState()
    {
        bool state = notificationObject.GetComponent<PhotoObject>().data.seen;
        
        return state;
    }

    void SetNotificationSystem()
    {
        notificationSystem.groups[NotificationType.Photo].notifications.Add(this);
        seenEvent.AddListener(delegate { notificationSystem.Seen(NotificationType.Photo); });
    }

    public override void ChangeNotification()
    {
        base.ChangeNotification();

        Debug.Log("wat ?");
        notificationObject.GetComponent<PhotoObject>().data.seen = true;
    }
}