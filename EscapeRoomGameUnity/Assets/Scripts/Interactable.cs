using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string objectName = "Egg";
    public bool canInteract = true;

    public virtual void Interact()
    {
        Debug.Log(objectName + " print from interactable");
    }
}
