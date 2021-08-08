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
    public AudioClip unlockedAudio;
    public AudioClip lockedAudio;
    public AudioSource playSource;
    public EventChecker requiredEvent;

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
        if (this.isLocked)
            playerInteraction.UpdateHelpText(name + " is currently LOCKED");
            //Debug.Log(name + " is currently locked");
        else
            playerInteraction.UpdateHelpText(name + " is UNLOCKED");
        //Debug.Log(name + " is currently unlocked");

        //base.Interact(playerInteraction);
    }

    public override void Use(Interactable targetItem, out bool useSuccess)
    {
        // player will insert the keys they picked up into the lock
        if (targetItem != null && insertedItem == null)
        {
            if (targetItem.TryGetComponent<Key>(out Key targetKeySpec))
            {
                insertedItem = targetKeySpec;
                targetKeySpec.storedObject = this;
                useSuccess = true;
                targetItem.transform.parent = this.transform;
                targetItem.transform.localPosition = new Vector3(0, 0, 0);
                targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                if (insertedItem == targetKey && requiredEvent.targetEvent.isCompleted == requiredEvent.isCompleted)
                {
                    //Debug.Log("The correct key was used");
                    insertedItem.transform.Rotate(new Vector3(0, 0, 90), Space.Self);
                    insertedItem.DisableItem();
                    StartCoroutine(DestroyKeyAfter());
                    this.isLocked = false;
                    playSource.clip = unlockedAudio;
                    playSource.Play();                    
                    // open door
                    targetDoor.UpdateLocks(this);
                    // prevent further interaction
                    this.DisableItem();
                    base.Interact();
                }
                else if (insertedItem == null)
                {
                    //Debug.Log("need to insert a key into the lock");
                    playSource.clip = lockedAudio;
                    playSource.Play();
                }
                else
                {
                    //Debug.Log("Wrong key was used, please try another key");
                    playSource.clip = lockedAudio;
                    playSource.Play();
                }
                return;
            }
        }
        useSuccess = false;
        
    }

    public bool IsLocked()
    {
        return this.isLocked;
    }

    public bool CheckIfCorrectItemInserted()
    {
        return false;
    }

    IEnumerator DestroyKeyAfter()
    {
        yield return new WaitForSeconds(1);
        Destroy(insertedItem.gameObject);
    }

    // currently not in use
    IEnumerator MovingKey(Quaternion a, Quaternion b, float curTime)
    {
        // move the key to the slot of the door handle
        // when done, call the turning key 
        yield return 0;
    }

    // currently not in use
    IEnumerator TurningKey(Quaternion a, Quaternion b, float curTime)
    {
        // turns the key when its inserted
        yield return 0;
    }
}
