using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class EventResponse
{
    public GameEvent gEvent;
    public UnityEvent response;

    public bool register = true;
}

public class GameEventListener : MonoBehaviour
{
    public List<EventResponse> eventResponses;

    private void OnEnable()
    {
        foreach(EventResponse rep in eventResponses)
        {
            if (rep.register) rep.gEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (EventResponse rep in eventResponses)
        {
            rep.gEvent.UnregisterListener(this);
        }
    }

    public void OnEventRaised(GameEvent gE)
    {
        foreach(EventResponse res in eventResponses)
        {
            if (res.gEvent == gE) res.response.Invoke();
        }
    }
}
