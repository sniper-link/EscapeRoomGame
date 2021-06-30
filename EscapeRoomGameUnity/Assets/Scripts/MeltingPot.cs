using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltingPot : MonoBehaviour
{
    public Interactable finishedProduct;

    private void Start()
    {
        finishedProduct.gameObject.SetActive(false);
    }

    public void ConvertItem()
    {
        finishedProduct.gameObject.SetActive(true);
    }
}
