using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public Door targetDoor;
    public Interactable key;
    public Interactable insertItem;
    public bool isFinished;

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (insertItem == key)
        {
            Debug.Log(name + " is unlocked");
            insertItem.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            insertItem.DisableItem();
            this.isFinished = true;
            // open door
            targetDoor.UpdateDoorLocks(this);
            // prevent further interaction
            this.enabled = false;
            this.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public override void Use(Interactable targetItem, out bool useSuccess)
    {

        if (targetItem != null && insertItem == null)
        {
            insertItem = targetItem;
            targetItem.storedObject = this;
            useSuccess = true;
            return;
        }
        useSuccess = false;
    }
}
