using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Storage
{
    public List<ScriptableObjectPage> pages = new List<ScriptableObjectPage>();
    public List<ScriptableObjectRune> runes = new List<ScriptableObjectRune>();

    public void AddItemToList(ScriptableObject data, ObjectType objectType)
    {
        switch (objectType)
        {
            case ObjectType.PAGE:
                ScriptableObjectPage page = (ScriptableObjectPage)data; 
                pages.Add(page);
                Debug.Log("Page with ID " + page.pageID + " has been picked up. Title: " + page.pageTitle + " Description: " + page.pageDescription);
                break;
            case ObjectType.RUNE: 
                ScriptableObjectRune rune = (ScriptableObjectRune)data; 
                runes.Add(rune);
                Debug.Log("Page with ID " + rune.runeID + " has been picked up.");
                break;
            default:
                break;
        }
    }

    public void OutputPages()
    {
        if (pages.Count != 0)
        {
            for (int i = 0; i < pages.Count; i++)
            {
                Debug.Log("Index " + i + " of Pages: " + pages[i].pageID + " " + pages[i].pageTitle + " " + pages[i].pageDescription);
            }
            return;
        }
        Debug.Log("No Pages in Storage");

    }

    public void OutputRunes()
    {
        if (runes.Count != 0)
        {
            for (int i = 0; i < runes.Count; i++)
            {
                Debug.Log("Index " + i + " of Runes: " + runes[i].runeID);
            }
            return;
        }
        Debug.Log("No Runes in Storage");
    }

    public void OutputStorage()
    {
        OutputPages();
        OutputRunes();
    }
}
