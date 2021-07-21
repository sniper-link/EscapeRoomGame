using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO::DO add animation to all of the actions
// TO::DO clean up code and make everything into functions

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInteraction : MonoBehaviour
{
    // use for interaction with objects around the map
    // add game physics to this later
    // left hand first
    public CameraController cameraController;
    public PlayerInventory playerInventory;
    public PlayerUI playerUI;
    public Transform leftHandPos;
    public Transform rightHandPos;
    public Transform twoHandPos;
    public Transform inspectPos;
    public GameObject dofEffect;
    public GameObject inspectCamera;
    public Interactable curInspectItem;
    public float tossForce = 100;
    public float playerVisionDis = 2;
    public float timeBetweenAction = 0f;
    Interactable targetItem = null;
    Side inspectSide = Side.Both;
    Interactable leftHandItemRef, rightHandItemRef;

    private void Awake()
    {
        // use this to check for stuff
        timeBetweenAction = 0f;
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (cameraController.cameraMode == CameraMode.PlayMode)
        {
            Ray playerVision = new Ray(cameraController.playerCamera.transform.position, cameraController.playerCamera.transform.forward);
            RaycastHit playerVisionEnd;

            if (Physics.Raycast(playerVision, out playerVisionEnd, playerVisionDis))
            {
                targetItem = playerVisionEnd.collider.GetComponentInParent<Collectable>();
                targetItem = playerVisionEnd.collider.GetComponentInParent<Interactable>();

                if (targetItem != null)
                {
                    //Debug.Log("looking at an interactable object");
                    if (targetItem.TryGetComponent<Door>(out Door loll))
                    {
                        targetItem = null;
                    }
                }
            }

            playerUI.ShowMBAction(targetItem != null, (targetItem != null ? targetItem.objectName : ""));

            playerInventory.GetInventoryItem(out leftHandItemRef, out rightHandItemRef);
            // left side will always be first
            {
                // TO::DO add a cooldown between pick ups

                if (Input.GetMouseButton(0))
                {
                    HandBehaviour(Side.Left, leftHandItemRef, leftHandPos);
                }

                // LMB pickup events
                if (Input.GetMouseButtonDown(0))
                {
                    PickupBehaviour(Side.Left, leftHandItemRef, leftHandPos);
                }

                // right clicks
                if (Input.GetMouseButton(1))
                {
                    HandBehaviour(Side.Right, rightHandItemRef, rightHandPos);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    PickupBehaviour(Side.Right, rightHandItemRef, rightHandPos);
                }

                playerUI.ShowHandMenu(Side.Left, Input.GetMouseButton(0) && leftHandItemRef != null);
                playerUI.ShowHandMenu(Side.Right, Input.GetMouseButton(1) && rightHandItemRef != null);
            }

            if (Input.GetKeyDown("e") && targetItem != null)
            {
                targetItem.Interact(this);
            }
        }
        else if (cameraController.cameraMode == CameraMode.InspectMode)
        {
            if (Input.GetKeyDown("f"))
            {
                ToggleDoFEffect(false);
                StopInspectObject();
                dofEffect.SetActive(false);
                inspectCamera.SetActive(false);
                return;
            }
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // TO::DO need to add rotation to the inspected objects base on the camera
            //Sets the Layer of the inspected object to "DoF" to only be rendered by the overlay camera
            ToggleDoFEffect(true);
            Vector3 oldRot = curInspectItem.transform.rotation.eulerAngles;
            curInspectItem.transform.rotation = Quaternion.Euler(oldRot + new Vector3(mouseY, -mouseX, 0f));
        }

        targetItem = null;

        if (Input.GetKeyDown("b"))
        {
            playerInventory.storage.OutputStorage();
        }
    }

    private void HandBehaviour(Side side, Interactable sideRef, Transform handPos)
    {
        if (sideRef != null)
        {
            playerUI.ShowHandMenu(side, sideRef != null);
            if (Input.GetKeyDown("q"))
            {
                sideRef.DropItem(handPos, out bool dropSuccess);
                if (dropSuccess)
                {
                    playerInventory.RemoveHandItem(side);
                    playerUI.ShowHandUI(side, false);
                }
            }
            if (Input.GetKeyDown("e"))
            {
                if (targetItem != null)
                {
                    targetItem.Use(sideRef, out bool useSuccess);
                    if (useSuccess)
                    {
                        playerInventory.RemoveHandItem(side);
                        playerUI.ShowHandUI(side, false);
                    }
                    return;
                }
                else
                {
                    // don't give the player the use option
                    //Debug.Log("Can't use the current hand item with anything");
                    UpdateHelpText("Can't use the current hand item with anything");
                }
            }
            if (Input.GetKeyDown("f"))
            {
                InspectModeEnable(side, sideRef);
            }
        }
    }

    private void ToggleDoFEffect(bool enable)
    {
        MeshRenderer[] meshes = curInspectItem.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in meshes)
        {
            if (enable)
            {
                item.gameObject.layer = 7;
                return;
            }
            item.gameObject.layer = 0;
        }
    }

    private void PickupBehaviour(Side side, Interactable sideRef, Transform handPos)
    {
        if (targetItem != null && targetItem.TryGetComponent(out Collectable collectable))
        {
            playerInventory.storage.AddItemToList(collectable.data, collectable.objectType);
            collectable.Interact(this);
        }

        if (sideRef == null)
        {
            if (targetItem != null && targetItem.canPickup)
            {
                playerInventory.AddHandItem(side, targetItem, handPos, out bool addSuccess);
                playerUI.ShowHandHint(side, addSuccess);
            }
        }
    }

    private void InspectModeEnable(Side side, Interactable sideRef)
    {
        Debug.Log("Inspecting Mode");
        dofEffect.SetActive(true);
        inspectCamera.SetActive(true);
        if (curInspectItem == null)
        {
            playerUI.ShowHandMenu(Side.Both, false);
            inspectSide = side;
            InspectObject(sideRef);
        }
    }

    public void InspectObject(Interactable targetItem)
    {
        playerUI.ShowHandMenu(Side.Both, false);
        curInspectItem = targetItem;
        cameraController.UpdateCameraMode(CameraMode.InspectMode);
        targetItem.transform.parent = inspectPos;
        targetItem.transform.SetPositionAndRotation(inspectPos.position, Quaternion.Euler(0, 0, 0));
        curInspectItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StopInspectObject()
    {

        Debug.Log("Quit Inspect Mode");
        cameraController.UpdateCameraMode(CameraMode.PlayMode);

        if (inspectSide == Side.Left)
        {
            curInspectItem.transform.parent = leftHandPos.transform;
            curInspectItem.transform.SetPositionAndRotation(leftHandPos.transform.position, Quaternion.Euler(0, 0, 0));
            curInspectItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            // ^ might be overkill
        }
        else if (inspectSide == Side.Right)
        {
            curInspectItem.transform.parent = rightHandPos.transform;
            curInspectItem.transform.SetPositionAndRotation(rightHandPos.transform.position, Quaternion.Euler(0, 0, 0));
            curInspectItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            // ^ might be overkill
        }
        else
        {
            Debug.LogWarning("Player Interaction is having trouble with inspect side");
        }

        curInspectItem = null;
    }

    public void UpdateHelpText(string newInfoText)
    {
        playerUI.UpdateHelpText(newInfoText);
    }
}
