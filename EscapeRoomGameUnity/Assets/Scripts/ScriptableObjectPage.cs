using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Page", menuName = "ScriptableObjects/Page", order = 1)]
public class ScriptableObjectPage : ScriptableObject
{
    public int pageID;
    public string pageName, pageTitle, pageDescription;
}
