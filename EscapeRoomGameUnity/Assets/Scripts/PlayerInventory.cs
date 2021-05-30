using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    // used to hold items players pickup
    // player will only be able to hold up to 
    // two-space worth of items at a time
    // ex. a candle holder and a key
    // or a table or something big
    // Start is called before the first frame update

    public Dictionary<string, string>playerInv = new Dictionary<string, string>(); // TO::DO Update variable name later
}
