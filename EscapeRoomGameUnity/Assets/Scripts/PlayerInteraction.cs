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
                //targetItem = playerVisionEnd.collider.GetComponentInParent<Collectable>();
                targetItem = playerVisionEnd.collider.GetComponentInParent<Interactable>();
                //targetItem = playerVisionEnd.collider.GetComponentInParent<NewInteractable>();

                if (targetItem != null)
                {
                    //Debug.Log("looking at an interactable object");
                    if (targetItem.TryGetComponent<Door>(out Door loll))
                    {
                        targetItem = null;
                    }
                }
            }

            playerUI.ShowMBAction(targetItem != null, (targetItem != null ? targetItem.itemName : ""));

            playerInventory.GetInventoryItem(out leftHandItemRef, out rightHandItemRef);
            // left side will always be first
            {
                // TO::DO add a cooldown between pick ups

                if (Input.GetMouseButton(0))
                {
                    HandAction(Side.Left, leftHandItemRef, leftHandPos);
                }

                // LMB pickup events
                if (Input.GetMouseButtonDown(0))
                {
                    PickupAction(Side.Left, leftHandItemRef, leftHandPos);
                }

                // right clicks
                if (Input.GetMouseButton(1))
                {
                    HandAction(Side.Right, rightHandItemRef, rightHandPos);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    PickupAction(Side.Right, rightHandItemRef, rightHandPos);
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
            // exit inspect mode
            if (Input.GetKeyDown("f"))
            {
                SetInspectView(false);
                return;
            }


            // updates inspected object base on mouse rotate
            // TO::DO show mouse while inspecting item 
            // so it looks like the player is draging the object
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // TO::DO need to add rotation to the inspected objects base on the camera
            // Sets the Layer of the inspected object to "DoF" to only be rendered by the overlay camera

            Vector3 oldRot = curInspectItem.transform.rotation.eulerAngles;
            curInspectItem.transform.rotation = Quaternion.Euler(oldRot + new Vector3(mouseY, -mouseX, 0f));
        }

        targetItem = null;

        if (Input.GetKeyDown("b"))
        {
            playerInventory.storage.OutputStorage();
        }
    }

    private void HandAction(Side itemSide, Interactable itemRef, Transform handPos)
    {
        // TO::DO get rid of Transform handPos from function arguement
        if (itemRef != null)
        {
            playerUI.ShowHandMenu(itemSide, itemRef != null);
            if (Input.GetKeyDown("q"))
            {
                itemRef.DropItem(handPos, out bool dropSuccess);
                if (dropSuccess)
                {
                    playerInventory.RemoveHandItem(itemSide);
                    playerUI.ShowHandUI(itemSide, false);
                }
            }
            if (Input.GetKeyDown("e"))
            {
                if (targetItem != null)
                {
                    targetItem.Use(itemRef, out bool useSuccess);
                    if (useSuccess)
                    {
                        playerInventory.RemoveHandItem(itemSide);
                        playerUI.ShowHandUI(itemSide, false);
                    }
                    return;
                }
                else
                {
                    UpdateHelpText("Can't use the current hand item with anything");
                }
            }
            if (Input.GetKeyDown("f"))
            {
                if (curInspectItem == null)
                    SetInspectView(true, itemSide, itemRef);
            }
        }
    }

    private void PickupAction(Side itemSide, Interactable itemRef, Transform handPos)
    {
        // TO::DO need to fix this, should not be using targetItem, rather itemRef
        if (targetItem != null && targetItem.TryGetComponent(out Collectable collectable))
        {
            playerInventory.storage.AddItemToList(collectable.data, collectable.objectType);
            collectable.Interact(this);
        }

        if (itemRef == null)
        {
            if (targetItem != null && targetItem.canPickup)
            {
                playerInventory.AddHandItem(itemSide, targetItem, handPos, out bool addSuccess);
                playerUI.ShowHandHint(itemSide, addSuccess);
            }
        }
    }

    private void SetInspectView(bool isActive, Side itemSide = Side.Both, Interactable itemRef = null)
    {
        MeshRenderer[] meshes = (curInspectItem ?? itemRef)?.GetComponentsInChildren<MeshRenderer>() ?? new MeshRenderer[] { };
        if (isActive)
        {
            playerUI.ShowHandMenu(Side.Both, false);
            curInspectItem = itemRef;
            inspectSide = itemSide;
            cameraController.UpdateCameraMode(CameraMode.InspectMode);
            curInspectItem.transform.parent = inspectPos;
            curInspectItem.transform.SetPositionAndRotation(inspectPos.position, Quaternion.Euler(0, 0, 0));
            curInspectItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (!isActive)
        {
            Debug.Log("Quit Inspect Mode");
            cameraController.UpdateCameraMode(CameraMode.PlayMode);

            curInspectItem.transform.parent = inspectSide == 0 ? leftHandPos : rightHandPos;
            curInspectItem.transform.localPosition = new Vector3(0, 0, 0);
            curInspectItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
            curInspectItem = null;
        }
        else
        {
            Debug.LogWarning("PlayerInteraction: Trouble going into inspect mode with current item");
        }

        inspectCamera.SetActive(isActive);
        foreach (var mesh in meshes)
        {
            mesh.gameObject.layer = (isActive ? 7 : 0);
        }
    }

    public void UpdateHelpText(string newInfoText)
    {
        playerUI.UpdateHelpText(newInfoText);
    }
}
