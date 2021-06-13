using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public bool isCompleted = false;
    public int containerSpace;
    public List<Interactable> requiredItems;
    public List<Interactable> storedItems;
    public Transform storeLocation;

    private void Start()
    {
        if (requiredItems.Count > containerSpace)
        {
            Debug.LogWarning(name + " has too many required items, please remove some reuired items");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable testInt))
        {
            this.AddToContainer(testInt);
        }
    }

    public void AddToContainer(Interactable newItem)
    {
        if (requiredItems.Contains(newItem))
        {
            requiredItems.Remove(newItem);
            storedItems.Add(newItem);
            newItem.transform.parent = storeLocation;
            newItem.transform.position = storeLocation.position;
            newItem.GetComponent<Rigidbody>().isKinematic = true;
            newItem.GetComponent<Rigidbody>().useGravity = false;
            newItem.GetComponent<Rigidbody>().mass = 0;
            //newItem.GetComponent<BoxCollider>().enabled = false;
            foreach (BoxCollider box in newItem.GetComponents<BoxCollider>())
            {
                //box.isTrigger = true;
                box.enabled = false;
            }
        }
    }
}
