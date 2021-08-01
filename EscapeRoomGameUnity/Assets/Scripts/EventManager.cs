using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class EventManager : MonoBehaviour
{
    List<Event> events = new List<Event>();
    Event currentEvent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in GetComponentsInChildren<Event>())
        {
            events.Add(item);
            Debug.Log(item.gameObject.name + ": has been added to the Event List");
            if (item.trigger.Count != 0)
            {
                foreach (var item2 in item.trigger)
                {
                    item2.OnInteract += HandleInterract;
                    Debug.Log(item2.gameObject.name + "Subscribed");
                }
            }
        }
        
    }
    
    void HandleInterract(NewInteractable interactable)
    {
        currentEvent = GetEvent(interactable);
        if (CheckRequiredCompleted(currentEvent) && CheckRequiredIncomplete(currentEvent))
        {
            if (currentEvent.activated && !currentEvent.switchActivated)
            {
                return;
            }
            currentEvent.OnActivate.Invoke(currentEvent);
            if (currentEvent.switchActivated)
            {
                currentEvent.activated = !currentEvent.activated;
                return;
            }
            currentEvent.activated = true;
        }

        foreach (var item in currentEvent.incompletes)
        {
            item.activated = false;
        }
        //UpdateAllEvents();
    }

    void UpdateAllEvents()
    {
        foreach (var item in events)
        {
            if (!CheckRequiredCompleted(item) || !CheckRequiredIncomplete(item))
            {
                item.OnActivate.Invoke(item);
            }
        }
    }

    bool CheckRequiredCompleted(Event currentEvent)
    {
        foreach (var item in currentEvent.requiredComplete)
        {
            if (item.activated)
            {
                continue;
            }
            return false;
        }
        return true;
    }

    bool CheckRequiredIncomplete(Event currentEvent)
    {
        foreach (var item in currentEvent.requiredIncomplete)
        {
            if (!item.activated)
            {
                continue;
            }
            return false;
        }
        return true;
    }



    Event GetEvent(NewInteractable interactable)
    {
        
        for (int i = 0; i < events.Count; i++)
        {
            for (int j = 0; j < events[i].trigger.Count; j++)
            {
                if (events[i].trigger[j] == interactable)
                {
                    currentEvent = events[i];
                }
            }
        }
        return currentEvent;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in events)
        {

        }
    }
}
