using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public UnityEvent<Event> OnActivate;
    [SerializeField] public UnityEvent<Event> OnDeactivate;
    public bool activated;
    public bool switchActivated;
    public List<Event> requiredComplete;
    public List<Event> requiredIncomplete;
    public List<Event> incompletes;
    public List<NewInteractable> trigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}