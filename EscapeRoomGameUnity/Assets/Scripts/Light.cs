using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public bool state = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (!state)
        {
            meshRenderer.material.color = Color.black;
        }
    }
    public void ChangeColor(int color)
    {
        switch (color)
        {
            case 1: meshRenderer.material.color = Color.red;
                break;
            case 2: meshRenderer.material.color = Color.green;
                break;
            default:
                break;
        }
    }

    public void ToggleLight()
    {
        if (state)
        {
            meshRenderer.material.color = Color.black;
            state = false;
            return;
        }
        meshRenderer.material.color = Color.white;
        state = true;
    }

    public void TurnOnLight()
    {
        meshRenderer.material.color = Color.white;
        state = true;
    }

    public void TurnOffLight()
    {
        meshRenderer.material.color = Color.black;
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
