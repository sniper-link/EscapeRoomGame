using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
public class NewInteractable : Interactable
{
    public event Action<NewInteractable> OnInteract;
    // Start is called before the first frame update

    public override void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Interacted");
        OnInteract?.Invoke(this);
    }
}
