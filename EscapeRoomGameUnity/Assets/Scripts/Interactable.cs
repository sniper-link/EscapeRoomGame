using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string objectName = "Egg";
    public bool canPickup = true;
    public Interactable storedObject;

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log(objectName + ": print from interactable");
        if (storedObject.TryGetComponent<Lock>(out Lock testLock))
        {
            Debug.Log("Lol");
            testLock.insertedItem = null;
        }
    }

    public void Inspect(PlayerInteraction playerInteraction)
    {
        
    }

    public virtual void Use(Interactable targetItem, out bool useSuccess)
    {
        storedObject = targetItem;
        targetItem.transform.parent = this.transform;
        targetItem.transform.localPosition = new Vector3(0, 0, 0);
        targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        useSuccess = true;
    }

    public void DropItem(Transform startLoc, out bool dropSuccess)
    {
        // ray cast to the ground and if there is space, put the item there
        Ray itemDropDis = new Ray(transform.position, new Vector3(0, -1, 0));
        RaycastHit itemDropEnd;
        dropSuccess = false;
        if (Physics.Raycast(itemDropDis, out itemDropEnd, 10))
        {
            this.transform.parent = null;
            this.transform.position = itemDropEnd.point;
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            dropSuccess = true;
            //playerInventory.RemoveLeftHandItem();
        }

        // TO::DO add a way to let the player place items on certain interactables
    }

    public void DisableItem()
    {
        // Disable all of the colliders
        Collider[] colliders = GetComponents<Collider>();
        Collider[] childColliders = GetComponentsInChildren<Collider>();


        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        foreach (Collider col in childColliders)
        {
            //Debug.Log(col.name + " is disabled");
            col.enabled = false;
        }
        this.enabled = false;
    }

    public virtual void RemoveFrom()
    {
        Debug.Log("calling from interactable");
    }
}
