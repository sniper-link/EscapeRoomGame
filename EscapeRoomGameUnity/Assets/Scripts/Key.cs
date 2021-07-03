using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public Lock parentLock;

    public override void Use(Interactable targetItem, out bool useSuccess)
    {
        /*storedObject = targetItem;
        targetItem.transform.parent = this.transform;
        targetItem.transform.localPosition = new Vector3(0, 0, 0);
        targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        useSuccess = true;*/
        useSuccess = true;
    }
}
