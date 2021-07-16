using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject crosshairStuff;
    public GameObject leftHandMenu;
    public GameObject rightHandMenu;
    public GameObject LMBIcon;
    public GameObject RMBIcon;
    public GameObject leftHandHint;
    public GameObject rightHandHint;
    public Text nameText;
    public Text infoText;

    private void Awake()
    {
        crosshairStuff.SetActive(false);
        leftHandMenu.SetActive(false);
        rightHandMenu.SetActive(false);
        leftHandHint.SetActive(false);
        rightHandHint.SetActive(false);
    }

    // UI Stuff
    public void ShowMBAction(bool newState, string nameOfItem)
    {
        nameText.text = nameOfItem;
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
        else if (targetSide == Side.Both)
        {
            leftHandMenu.SetActive(newState);
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
        else if (targetSide == Side.Both)
        {
            leftHandHint.SetActive(newState);
            rightHandHint.SetActive(newState);
        }
    }

    public void ShowHandUI(Side targetSide, bool newState)
    {
        ShowHandHint(targetSide, newState);
        ShowHandMenu(targetSide, newState);
    }

    public void UpdateHelpText(string helpText)
    {
        infoText.text = helpText;
        StartCoroutine(DisappearText(2));
    }

    IEnumerator DisappearText(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        infoText.text = "";
    }
}
