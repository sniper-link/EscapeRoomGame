using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// use a serializable sturct to get the state of things
[System.Serializable]
public struct EventChecker
{
    public CustomEvent targetEvent;
    public bool isCompleted;
}

public class CustomEvent : MonoBehaviour
{
    public UnityEvent OnCompeleteEvent;
    public UnityEvent OnResetEvent;
    public Interactable targetInteractable;
    public List<EventChecker> requiredEvents;
    public bool isCompleted = false;
    public bool canReset = false;
    public bool resetAfterComplete = false;
    public bool useResetEventOnUncomplete = false;

    public event Action<CustomEvent> OnCompleted;

    private void Awake()
    {
        targetInteractable.OnInteracted += HandleInteracted;
    }

    private void HandleInteracted(Interactable targetItem)
    {
        if (requiredEvents.Count > 0)
        {
            foreach (var reqEvent in requiredEvents)
            {
                if (reqEvent.targetEvent.isCompleted != reqEvent.isCompleted)
                {
                    // if one of the requirements is not met, then this current event won't be complete
                    return;
                }
            }
        }

        if (!isCompleted)
        {
            OnCompeleteEvent.Invoke();
            OnCompleted?.Invoke(this);
            isCompleted = true;
        }
        else if (isCompleted && canReset)
        {
            OnResetEvent.Invoke();
            isCompleted = false;
        }
    }

    public void SetEventComplete()
    {
        isCompleted = true;
        OnCompeleteEvent.Invoke();
    }
    
    public void SetEventUncomplete()
    {
        isCompleted = false;
        if (useResetEventOnUncomplete)
        {
            OnResetEvent.Invoke();
        }
    }

    public void ResetEvent()
    {
        isCompleted = false;
        OnResetEvent.Invoke();
        // reset the targetInteractable
    }
}