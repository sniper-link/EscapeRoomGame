using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    // Start is called before the first frame update
    public bool isOpen = false;
    //public bool canInteract = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        Debug.Log("Door Open");
        if (canInteract)
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
        
    }

    IEnumerator DoorOpening(Quaternion a, Quaternion b, float curTime)
    {
        this.transform.rotation = Quaternion.Slerp(a, b, curTime / 1);
        if (curTime <= 1f)
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
