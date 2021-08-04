using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventManager : MonoBehaviour
{
    public UnityEvent OnFinalEvents;
    public List<CustomEvent> tiedEvents = new List<CustomEvent>();
    public bool inSequence = false;
    Queue<CustomEvent> queuedEvents = new Queue<CustomEvent>();

    private void Awake()
    {
        foreach (var events in tiedEvents)
        {
            events.OnCompleted += HandleActivateEvent;
        }
    }

    public void HandleActivateEvent(CustomEvent customEvent)
    {
        Debug.Log("onComplete evne");
        queuedEvents.Enqueue(customEvent);
        foreach(var tiedEve in tiedEvents)
        {
            if (!queuedEvents.Contains(tiedEve))
            {
                return;
            }
        }

        // if all events are complete
        OnFinalEvents.Invoke();
    }
}
