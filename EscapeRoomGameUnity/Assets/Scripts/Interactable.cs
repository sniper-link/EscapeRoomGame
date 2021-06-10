using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string objectName = "Egg";
    public bool canInteract = true;
    public bool canPickup = true;
    public bool twoHandItem = false;
    public Transform twoHandPos;

    public virtual void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log(objectName + ": print from interactable");
    }
}
