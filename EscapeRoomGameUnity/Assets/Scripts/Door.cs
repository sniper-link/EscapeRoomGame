using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeOfDoor
{
    Normal = 0,
    KeyNLock = 1,
    Combination = 2,
    Activatiable = 3
}

public class Door : Interactable
{
    // Start is called before the first frame update
    public bool isOpen = false;
    //public bool canInteract = true;
    public float timeToOpen = 1f;
    public Interactable itemToUnlockWith;
    [SerializeField]
    TypeOfDoor typeOfDoor;

    void Start()
    {
        canPickup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Door Open");
        if (canInteract)
        {
            if (itemToUnlockWith == null)
            {
                if (!isOpen)
                {
                    Vector3 oldRotation = this.transform.eulerAngles;
                    Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y + 90, oldRotation.z);
                    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 1f);
                    isOpen = true;
                    canInteract = false;
                    StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
                    //this.canInteract = true;
                }
                else
                {
                    Vector3 oldRotation = this.transform.eulerAngles;
                    Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y - 90, oldRotation.z);
                    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 1f);
                    isOpen = false;
                    canInteract = false;
                    StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
                    //this.canInteract = true;
                }
            }
            else if (itemToUnlockWith != null)
            {
                playerInteraction.playerInventory.GetLeftHandItem(out Interactable leftHandItem);
                playerInteraction.playerInventory.GetRightHandItem(out Interactable rightHandItem);
                if (itemToUnlockWith == leftHandItem)
                {
                    itemToUnlockWith = null;
                    Destroy(leftHandItem.gameObject);
                    playerInteraction.playerInventory.RemoveLeftHandItem();
                    Vector3 oldRotation = this.transform.eulerAngles;
                    Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y + 90, oldRotation.z);
                    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 1f);
                    isOpen = true;
                    canInteract = false;
                    StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
                    
                }
                else if (itemToUnlockWith == rightHandItem)
                {
                    itemToUnlockWith = null;
                    Destroy(rightHandItem.gameObject);
                    playerInteraction.playerInventory.RemoveRightHandItem();
                    Vector3 oldRotation = this.transform.eulerAngles;
                    Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y + 90, oldRotation.z);
                    //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, newRotation, 1f);
                    isOpen = true;
                    canInteract = false;
                    StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
                    
                }
                else
                {
                    Debug.Log("Can't unlock door");
                }
            }
        }
        
    }

    IEnumerator DoorOpening(Quaternion a, Quaternion b, float curTime)
    {
        this.transform.rotation = Quaternion.Slerp(a, b, curTime / timeToOpen);
        if (curTime <= timeToOpen)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            curTime += Time.deltaTime;
            StartCoroutine(DoorOpening(a, b, curTime));
        }
        else
        {
            Debug.Log("done opening");
            canInteract = true;
        }
        yield return 0;
        
    }
}
