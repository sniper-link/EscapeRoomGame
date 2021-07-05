using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public GameObject crosshairStuff;
    public GameObject leftHandMenu;
    public GameObject rightHandMenu;
    public GameObject LMBIcon;
    public GameObject RMBIcon;
    public GameObject leftHandHint;
    public GameObject rightHandHint;

    private void Awake()
    {
        crosshairStuff.SetActive(false);
        leftHandMenu.SetActive(false);
        rightHandMenu.SetActive(false);
        leftHandHint.SetActive(false);
        rightHandHint.SetActive(false);
    }
    // UI Stuff

    public void ShowMBAction(bool newState)
    {
        crosshairStuff.SetActive(newState);
    }

    public void ShowLeftHandMenu(bool newState)
    {
        leftHandMenu.SetActive(newState);
    }

    public void ShowHandMenu(Side targetSide, bool newState)
    {
        if (targetSide == Side.Left)
        {
            leftHandMenu.SetActive(newState);
        }
        else if (targetSide == Side.Right)
        {
            rightHandMenu.SetActive(newState);
        }
    }

    public void ShowHandHint(Side targetSide, bool newState)
    {
        if (targetSide == Side.Left)
        {
            leftHandHint.SetActive(newState);
        }
        else if (targetSide == Side.Right)
        {
            rightHandHint.SetActive(newState);
        }
    }
}
