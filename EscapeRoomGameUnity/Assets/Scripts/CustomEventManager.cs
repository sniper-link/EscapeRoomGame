using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CustomEventManager : MonoBehaviour
{
    public UnityEvent OnFinalEvents;
    public List<EventDispatcher> tiedEvents = new List<EventDispatcher>();
    public bool inSequence = false;
    public Queue<EventDispatcher> queuedEvents = new Queue<EventDispatcher>();
    public EventDispatcher[] showList;

    private void Awake()
    {
        foreach (var events in tiedEvents)
        {
            events.OnCompleted += HandleActivateEvent;
        }
    }

    public void HandleActivateEvent(EventDispatcher targetEvent)
    {
        //Debug.Log("onComplete evne" + targetEvent);
        if (!queuedEvents.Contains(targetEvent))
        {
            queuedEvents.Enqueue(targetEvent);
        }
        
        showList = queuedEvents.ToArray();
        /*foreach (var tiedEven in queuedEvents)
        {
            //if (queuedEvents)
        }*/
        if (tiedEvents.Count == showList.Length)
        {
            if (tiedEvents.SequenceEqual(showList))
            {
                OnFinalEvents.Invoke();
            }
            else
            {
                queuedEvents.Clear();
                foreach (var tiedEve in tiedEvents)
                {
                    tiedEve.ResetEvent();
                }
            }
            // if all events are complete
            
        }
    }
}
