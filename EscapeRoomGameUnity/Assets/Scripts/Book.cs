using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Book : MonoBehaviour
{
    public List<ScriptableObjectPage> pages;
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Journalpage").GetComponent<Collectable>().Interact(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteraction>());
    }

    public void AddItemToList(ScriptableObject data)
    {
        ScriptableObjectPage yes = (ScriptableObjectPage)data;
        pages.Add(yes);
        Debug.Log("Page with ID " + yes.pageID + " has been picked up. Title: " + yes.pageTitle + " Description: " + yes.pageDescription);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
