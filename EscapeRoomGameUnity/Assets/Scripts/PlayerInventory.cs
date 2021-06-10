using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    // used to hold items players pickup
    // player will only be able to hold up to 
    // two-space worth of items at a time
    // ex. a candle holder and a key
    // or a table or something big
    // Start is called before the first frame update

    [SerializeField]
    private Interactable leftHandItem, rightHandItem;

    public void AddLeftHandItem(Interactable targetItem, out bool addSuccess)
    {
        if (leftHandItem == null)
        {
            leftHandItem = targetItem;
            addSuccess = true;
        }
        else
        {
            addSuccess = false;
        }
    }

    public void AddRightHandItem(Interactable targetItem, out bool addSuccess)
    {
        if (rightHandItem == null)
        {
            rightHandItem = targetItem;
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

    /*
    public void AddItem(Interactable targetItem, out bool addSuccess)
    {
        if (rightHandItem)
        {
            if (leftHandItem)
            {
                Debug.LogWarning("WARN: Both Left and Right hand already have an item");
                addSuccess = false;
            }
            else
            {
                leftHandItem = targetItem;
                addSuccess = true;
            }
        }
        else
        {
            rightHandItem = targetItem;
            addSuccess = true;
        }
    }*/

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

    public void GetLeftHandItem(out Interactable itemRef)
    {
        itemRef = this.leftHandItem;
    }

    public void GetRightHandItem(out Interactable itemRef)
    {
        itemRef = this.rightHandItem;
    }
}
