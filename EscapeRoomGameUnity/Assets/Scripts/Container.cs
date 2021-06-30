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
    public Interactable finishedProduct;

    private void Start()
    {
        finishedProduct.gameObject.SetActive(false);
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
            //newItem.GetComponent<Rigidbody>().isKinematic = true;
            //newItem.GetComponent<Rigidbody>().useGravity = false;
            //newItem.GetComponent<Rigidbody>().mass = 0;
            //newItem.GetComponent<BoxCollider>().enabled = false;
            foreach (BoxCollider box in newItem.GetComponents<BoxCollider>())
            {
                //box.isTrigger = true;
                box.enabled = false;
            }
        }
    }

    public override void Interact(PlayerInteraction playerInteraction)
    {

    }

    public override void Use(Interactable targetItem, out bool useSuccess)
    {
        Debug.Log("from conta");
        // if successful, item has been added to the container, else return false
        if (targetItem != null && storedItems.Count < containerSpace && !isCompleted)
        {
            Debug.Log("next 1");
            if (requiredItems.Contains(targetItem))
            {
                //requiredItems.Remove(targetItem);
                storedItems.Add(targetItem);
                targetItem.transform.parent = storeLocation;
                targetItem.transform.position = storeLocation.position;
                if (storedItems.Count == containerSpace)
                {
                    isCompleted = true;
                }
                else
                {
                    Debug.Log("Container Error");
                }
            }
            useSuccess = true;
            return;
        }
        useSuccess = false;
    }

    public void ConvertItem()
    {
        foreach (Interactable item in storedItems)
        {
            if (!storedItems.Contains(item))
            {
                return;
            }
        }
        finishedProduct.gameObject.SetActive(true);
    }

}
