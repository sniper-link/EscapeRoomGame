using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Door : Unlockable
{
    // Start is called before the first frame update
    public bool isOpen = false;
    public bool canInteract = false;
    public float timeToOpen = 1f;

    void Start()
    {
        canPickup = false;
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        //                                              V maybe need this V
        if (canInteract && requiredLocks.Count == 0 /*&& completedLocks.Count == 0*/)
        {
            Vector3 oldRotation = this.transform.eulerAngles;
            Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y + (isOpen ? -90 : 90), oldRotation.z);
            isOpen = !isOpen;
            canInteract = false;
            //StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
        }
    }

    public override void Open()
    {
        OpenDoor();
    }

    public void OpenDoor(float delayTime=0)
    {
        Vector3 oldRotation = this.transform.eulerAngles;
        Quaternion newRotation = Quaternion.Euler(oldRotation.x, oldRotation.y + (isOpen ? -90f : 90), oldRotation.z);
        isOpen = !isOpen;
        canInteract = false;
        StartCoroutine(Delay(this.transform.rotation, newRotation, delayTime));
        //StartCoroutine(DoorOpening(this.transform.rotation, newRotation, 0f));
    }

    IEnumerator Delay(Quaternion a, Quaternion b, float targetTime)
    {
        yield return new WaitForSeconds(targetTime);
        StartCoroutine(DoorOpening(a, b, 0f));
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
            // TO::DO check this later
            //Debug.Log("done opening");
            //canInteract = true;
        }
        yield return 0;
        
    }
}
