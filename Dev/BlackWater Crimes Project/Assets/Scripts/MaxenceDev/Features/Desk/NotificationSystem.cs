using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public enum NotificationType { Photo, Report }

[Serializable]
public class NotificationGroup
{
    public List<Notification> notifications = new List<Notification>();

    public GameObject tabNotification;

    public int unseenNotifications;
}

public class NotificationSystem : SerializedMonoBehaviour
{
    public Dictionary<NotificationType, NotificationGroup> groups = new Dictionary<NotificationType, NotificationGroup>();

    void Start()
    {
        foreach (NotificationGroup group in groups.Values)
        {
            foreach (Notification notification in group.notifications)
            {
                if (!notification.isSeen) group.unseenNotifications++;
            }

            group.tabNotification.GetComponentInChildren<Text>().text = group.unseenNotifications.ToString();

            if (group.unseenNotifications <= 0) group.tabNotification.SetActive(false);
        }
    }

    public void Seen(NotificationType _type)
    {
        NotificationGroup _group = groups[_type];

        _group.unseenNotifications--;
        _group.tabNotification.GetComponentInChildren<Text>().text = _group.unseenNotifications.ToString();

        if (_group.unseenNotifications <= 0) _group.tabNotification.SetActive(false);
    }
}
