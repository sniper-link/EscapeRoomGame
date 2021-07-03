using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Interactable
{
    public Door targetDoor;
    public Unlockable targetUnlockable;
    public Key targetKey;
    public Key insertedItem;
    public bool isLocked = true;

    // TO::DO add key variables instead of Interactable variable

    private void Start()
    {
        if (isLocked && targetKey == null)
        {
            // give a warning message when a lock is locked but it doesn't have anything
            // to unlock the lock
            Debug.LogWarning("Lock: " + name + " is locked " +
                "but doesn't have a target key to unlock it.");
        }
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        if (insertedItem == targetKey)
        {
            Debug.Log("The correct key was used");
            insertedItem.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
            insertedItem.DisableItem();
            this.isLocked = false;
            // open door
            targetDoor.UpdateLocks(this);
            // prevent further interaction
            this.DisableItem();
        }
        else if (insertedItem == null)
        {
            Debug.Log("need to insert a key into the lock");
        }
        else
        {
            Debug.Log("Wrong key was used, please try another key");
        }
    }

    public override void Use(Interactable targetItem, out bool useSuccess)
    {
        // player will insert the keys they picked up into the lock
        if (targetItem != null && insertedItem == null)
        {
            if (targetItem.TryGetComponent<Key>(out Key targetKey))
            {
                insertedItem = targetKey;
                targetKey.storedObject = this;
                useSuccess = true;
                targetItem.transform.parent = this.transform;
                targetItem.transform.localPosition = new Vector3(0, 0, 0);
                targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                return;
            }
        }
        useSuccess = false;
        
    }

    public bool IsLocked()
    {
        return this.isLocked;
    }
}
