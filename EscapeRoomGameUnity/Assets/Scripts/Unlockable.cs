using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class has a lock and can only be unlocked when all of the locks are
// unlocked

public class Unlockable : Interactable
{
    public List<Lock> requiredLocks;
    public List<Lock> completedLocks;

    // passes in the lock that just got unlocked and check if every lock
    // on the unlockable object is unlocked, if yes then it will call the 
    // open function
    public void UpdateLocks(Lock targetLock)
    {
        if (requiredLocks.Contains(targetLock) && !targetLock.IsLocked())
        {
            requiredLocks.Remove(targetLock);
            completedLocks.Add(targetLock);
            if (requiredLocks.Count == 0)
            {
                Open();
                //Debug.Log("opening unlockable");
            }
        }
    }

    public virtual void Open()
    {
        Debug.Log("Opening from unlockable script");
    }
}
