using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationReport : Notification
{
    public GameObject informationObject;

    void Start()
    {
        if (!GetNotificationState())
        {
            _notification = InstantiateNotification(notificationObject.transform);
        }
    }

    protected override bool GetNotificationState()
    {
        bool state = informationObject.GetComponent<ReportObject>().data.seen;

        isSeen = state;

        return state;
    }

    public override void ChangeNotification()
    {
        base.ChangeNotification();

        informationObject.GetComponent<ReportObject>().data.seen = false;
    }
}
