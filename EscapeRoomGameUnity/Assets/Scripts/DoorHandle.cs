using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : Interactable
{
    public Door targetDoor;
    public Interactable key;
    public Interactable insertItem;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (insertItem == key)
        {
            insertItem.canPickup = false;
            // open door
            targetDoor.OpenDoor();
            // prevent further interaction
            this.enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public override void Use(Interactable targetItem)
    {
        if (targetItem != null)
        {
            insertItem = targetItem;
        }
    }
}
