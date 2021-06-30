using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : Interactable
{
    public Transform placeLocation;
    public Container containerRef;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (containerRef != null)
        {
            containerRef.ConvertItem();
        }
    }

    public override void Use(Interactable targetItem, out bool useSuccess)
    {
        if(containerRef == null && targetItem.TryGetComponent(out Container contRef))
        {
            containerRef = contRef;
            useSuccess = true;
            return;
        }
        useSuccess = false;
    }

}
