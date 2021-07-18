using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    public ObjectType objectType;
    public ScriptableObject data;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Interacted with Collectable");
        GameObject.Destroy(gameObject);
    }
}
