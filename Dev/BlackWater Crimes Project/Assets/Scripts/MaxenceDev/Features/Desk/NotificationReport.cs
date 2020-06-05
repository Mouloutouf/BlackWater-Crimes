using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationReport : Notification
{
    public GameObject informationObject;

    void Start()
    {
        isSeen = GetNotificationState();

        if (!isSeen) _notification = InstantiateNotification(notificationObject.transform);

        SetNotificationSystem();
    }

    protected override bool GetNotificationState()
    {
        bool state = informationObject.GetComponent<ReportObject>().data.seen;
        
        return state;
    }

    void SetNotificationSystem()
    {
        notificationSystem.groups[NotificationType.Report].notifications.Add(this);
        seenEvent.AddListener(delegate { notificationSystem.Seen(NotificationType.Report); });
    }

    public override void ChangeNotification()
    {
        base.ChangeNotification();

        informationObject.GetComponent<ReportObject>().data.seen = true;
    }
}
