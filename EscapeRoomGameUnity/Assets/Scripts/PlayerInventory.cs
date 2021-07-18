using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Storage storage = new Storage();
    // used to hold items players pickup
    // player will only be able to hold up to 
    // two-space worth of items at a time
    // ex. a candle holder and a key
    // or a table or something big

    [SerializeField]
    private Interactable leftHandItem, rightHandItem;

    public void AddLeftHandItem(Interactable targetItem, Transform handLoc, out bool addSuccess)
    {
        if (leftHandItem == null)
        {
            leftHandItem = targetItem;
            if (targetItem.storedObject != null && targetItem.storedObject.TryGetComponent<Lock>(out Lock testLock))
            {
                testLock.insertedItem = null;
            }

            targetItem.GetComponent<Rigidbody>().isKinematic = true;
            targetItem.transform.parent = handLoc;
            targetItem.transform.localPosition = new Vector3(0, 0, 0);
            targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            // TO::DO add animation for raising hand

            addSuccess = true;
        }
        else
        {
            addSuccess = false;
        }
    }

    public void AddRightHandItem(Interactable targetItem, Transform handLoc, out bool addSuccess)
    {
        if (rightHandItem == null)
        {
            rightHandItem = targetItem;
            if (targetItem.storedObject != null && targetItem.storedObject.TryGetComponent<Lock>(out Lock testLock))
            {
                testLock.insertedItem = null;
            }

            targetItem.GetComponent<Rigidbody>().isKinematic = true;
            targetItem.transform.parent = handLoc;
            targetItem.transform.localPosition = new Vector3(0, 0, 0);
            targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            // TO::DO add animation for raising hand

            addSuccess = true;
        }
        else
        {
            addSuccess = false;
        }
    }

    public void AddTwoHandItem(Interactable targetItem, out bool addSuccess)
    {
        // add checks for both left hand and right item
        if (leftHandItem == null && rightHandItem == null)
        {
            leftHandItem = targetItem;
            rightHandItem = targetItem;
            addSuccess = true;
        }
        else
        {
            addSuccess = false;
        }
    }

    public void GetBothHandItem(out Interactable leftHandItem, out Interactable rightHandItem)
    {
        leftHandItem = this.leftHandItem;
        rightHandItem = this.rightHandItem;
    }

    public void RemoveLeftHandItem()
    {
        leftHandItem = null;
    }

    public void RemoveRightHandItem()
    {
        rightHandItem = null;
    }

    public void RemoveTwoHandItem()
    {
        leftHandItem = null;
        rightHandItem = null;
    }

    public void RemoveItem(Side side)
    {
        if (side == Side.Left)
            leftHandItem = null;
        else if (side == Side.Right)
            rightHandItem = null;
        else if (side == Side.Both)
        {
            leftHandItem = (rightHandItem = null);
        }
    }

    public void GetLeftHandItem(out Interactable itemRef)
    {
        itemRef = this.leftHandItem;
    }

    public void GetRightHandItem(out Interactable itemRef)
    {
        itemRef = this.rightHandItem;
    }

    public void GetInventoryItem(out Interactable leftHandItemRef, out Interactable rightHandItemRef)
    {
        leftHandItemRef = this.leftHandItem;
        rightHandItemRef = this.rightHandItem;
    }

    
}
