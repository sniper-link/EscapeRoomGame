using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

// use a serializable sturct to get the state of things
[System.Serializable]
public struct EventChecker
{
    public EventDispatcher targetEvent;
    public bool isCompleted;
}

public class EventDispatcher : MonoBehaviour
{
    [Tooltip("These Event(s) will be called when this event completes")]
    public UnityEvent OnCompeleteEvent;
    [Tooltip("These Event(s) will be called when this event resets")]
    public UnityEvent OnResetEvent;
    public Interactable targetInteractable;
    public List<EventChecker> requiredEvents;
    public bool isCompleted = false;
    public bool canReset = true;

    public event Action<EventDispatcher> OnCompleted;

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
            isCompleted = true;
            OnCompleted?.Invoke(this);
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
        if (canReset)
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

    public void ClearEvent()
    {
        Debug.Log("Clear Event");
        OnCompeleteEvent = new UnityEvent();
        OnResetEvent = new UnityEvent();
        targetInteractable = null;
        requiredEvents = new List<EventChecker>();
    }

    public void DisableEvent()
    {
        canReset = false;
    }
}

/*
[CustomEditor(typeof(EventDispatcher))]
public class EventDispatcherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the attribute from the EventDispatcher Class
        base.OnInspectorGUI();

        // Draw Custom layouts

        // Draw a button that will reset events in the target EventDispatcher
        if (GUILayout.Button("Clear Event"))
        {
            ((EventDispatcher)target).ClearEvent();
        }
    }
}*/

