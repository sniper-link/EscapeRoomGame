using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : Interactable
{
    public bool isCompleted = false;
    public int containerSpace;
    public List<Interactable> requiredItems;
    public List<Interactable> storedItems;
    private List<Interactable> testList = new List<Interactable>();

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
        }
    }
}
