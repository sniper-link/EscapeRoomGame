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
    public Interactable curInspectItem;
    public float tossForce = 100;
    public float playerVisionDis = 2;
    public float timeBetweenAction = 0f;
    
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
            Interactable targetItem = null;
            Ray playerVision = new Ray(cameraController.playerCamera.transform.position, cameraController.playerCamera.transform.forward);
            RaycastHit playerVisionEnd;

            if (Physics.Raycast(playerVision, out playerVisionEnd, playerVisionDis))
            {
                targetItem = playerVisionEnd.collider.GetComponentInParent<Interactable>();
                if (targetItem != null)
                {
                    //Debug.Log("looking at an interactable object");
                }

            }

            playerUI.ShowMBAction(targetItem != null);


            // left side will always be first
            {
                // TO::DO add a cooldown between pick ups
                
                if (Input.GetMouseButton(0))
                {
                    playerInventory.GetLeftHandItem(out Interactable itemRef);
                    
                    if (itemRef == null)
                    {
                        if (targetItem != null && targetItem.canPickup)
                        {
                            playerInventory.AddLeftHandItem(targetItem, leftHandPos, out bool addSuccess);
                            /*if (addSuccess)
                            {
                                targetItem.GetComponent<Rigidbody>().isKinematic = true;
                                targetItem.transform.parent = leftHandPos;
                                targetItem.transform.localPosition = new Vector3(0, 0, 0);
                                targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                // TO::DO add animation for raising hand
                            }*/
                        }
                    }
                    else
                    {
                        playerUI.ShowHandMenu(Side.Left, itemRef != null);
                        if (Input.GetKeyDown("q"))
                        {
                            itemRef.DropItem(leftHandPos, out bool dropSuccess);
                            if (dropSuccess)
                            {
                                playerInventory.RemoveLeftHandItem();
                            }
                            // ray cast to the ground and if there is space, put the item there
                            /*Ray itemDropDis = new Ray(leftHandPos.transform.position, transform.up * -1);
                            RaycastHit itemDropEnd;
                            if (Physics.Raycast(itemDropDis, out itemDropEnd, 10))
                            {
                                itemRef.transform.parent = null;
                                itemRef.transform.position = itemDropEnd.point;
                                itemRef.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                playerInventory.RemoveLeftHandItem();
                            }*/
                        }
                        if (Input.GetKeyDown("e"))
                        {
                            if (targetItem != null)
                            {
                                targetItem.Use(itemRef, out bool useSuccess);
                                /*(itemRef.transform.parent = targetItem.transform;
                                itemRef.transform.localPosition = new Vector3(0, 0, 0);
                                itemRef.transform.localRotation = Quaternion.Euler(0, 0, 0);*/
                                if (useSuccess)
                                {
                                    playerInventory.RemoveLeftHandItem();
                                }
                                return;
                            }
                        }
                        if (Input.GetKeyDown("f"))
                        {
                            Debug.Log("Inspecting Mode");
                            if (curInspectItem == null)
                            {
                                InspectObject(itemRef);
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    playerUI.ShowHandMenu(Side.Left, false);
                }

                if (Input.GetMouseButton(1))
                {
                    playerInventory.GetRightHandItem(out Interactable itemRef);
                    
                    if (itemRef == null)
                    {
                        if (targetItem != null && targetItem.canPickup)
                        {
                            playerInventory.AddRightHandItem(targetItem, rightHandPos, out bool addSuccess);
                            /*if (addSuccess)
                            {
                                targetItem.GetComponent<Rigidbody>().isKinematic = true;
                                targetItem.transform.parent = rightHandPos;
                                targetItem.transform.localPosition = new Vector3(0, 0, 0);
                                targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            }*/
                        }
                    }
                    else
                    {
                        playerUI.ShowHandMenu(Side.Right, itemRef != null);
                        if (Input.GetKeyDown("q"))
                        {
                            itemRef.DropItem(leftHandPos, out bool dropSuccess);
                            if (dropSuccess)
                            {
                                playerInventory.RemoveLeftHandItem();
                            }
                            // ray cast to the ground and if there is space, put the item there
                            /*Ray itemDropDis = new Ray(rightHandPos.transform.position, transform.up * -1);
                            RaycastHit itemDropEnd;
                            if (Physics.Raycast(itemDropDis, out itemDropEnd, 10))
                            {
                                itemRef.transform.parent = null;
                                itemRef.transform.position = itemDropEnd.point;
                                itemRef.transform.localRotation = Quaternion.Euler(0, 0, 0);
                                playerInventory.RemoveRightHandItem();
                            }*/
                        }
                        if (Input.GetKeyDown("e"))
                        {
                            if (targetItem != null)
                            {
                                targetItem.Use(itemRef, out bool useSuccess);
                                /*(itemRef.transform.parent = targetItem.transform;
                                itemRef.transform.localPosition = new Vector3(0, 0, 0);
                                itemRef.transform.localRotation = Quaternion.Euler(0, 0, 0);*/
                                if (useSuccess)
                                {
                                    playerInventory.RemoveLeftHandItem();
                                }
                            }
                        }
                        if (Input.GetKeyDown("f"))
                        {
                            Debug.Log("Inspecting Mode");
                            if (curInspectItem == null)
                            {
                                InspectObject(itemRef);
                            }

                        }
                    }
                }
                else if (Input.GetMouseButtonUp(1))
                {
                    //playerUI.ShowLeftHandMenu(false);
                    playerUI.ShowHandMenu(Side.Right, false);
                }
            }

            if (Input.GetKeyDown("e") && targetItem != null)
            {
                targetItem.Interact(this);
            }

            /*if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                playerUI.ShowHandMenu(Side.Left, Input.GetMouseButton(0));
                playerUI.ShowHandMenu(Side.Right, Input.GetMouseButton(1));


            }*/

        }
        else if (cameraController.cameraMode == CameraMode.InspectMode)
        {
            //playerInventory.GetLeftHandItem(out Interactable itemRef);
            if (Input.GetKeyDown("f"))
            {
                StopInspectObject();
                return;
            }
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // TO::DO need to add rotation to the inspected objects base on the camera
            Vector3 oldRot = curInspectItem.transform.rotation.eulerAngles;
            curInspectItem.transform.rotation = Quaternion.Euler(oldRot + new Vector3(mouseY, -mouseX, 0f));
        }
    }

    public void InspectObject(Interactable targetItem)
    {
        curInspectItem = targetItem;
        cameraController.UpdateCameraMode(CameraMode.InspectMode);
        targetItem.transform.parent = inspectPos;
        targetItem.transform.SetPositionAndRotation(inspectPos.position, Quaternion.Euler(0, 0, 0));
    }

    public void StopInspectObject()
    {
        
        Debug.Log("Quit Inspect Mode");
        cameraController.UpdateCameraMode(CameraMode.PlayMode);

        curInspectItem.transform.parent = leftHandPos.transform;
        curInspectItem.transform.SetPositionAndRotation(leftHandPos.transform.position, Quaternion.Euler(0, 0, 0));
        curInspectItem = null;
    }
}
