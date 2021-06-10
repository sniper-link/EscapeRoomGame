using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInteraction : MonoBehaviour
{
    // use for interaction with objects around the map
    // add game physics to this later
    // left hand first
    public CameraController cameraController;
    public PlayerInventory playerInventory;
    public Transform leftHandPos;
    public Transform rightHandPos;
    public Transform twoHandPos;
    public float tossForce = 100;
    
    private void Awake()
    {
        // use this to check for stuff
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Interactable targetItem = null;
        Ray playerVision = new Ray(cameraController.playerCamera.transform.position, cameraController.playerCamera.transform.forward);
        RaycastHit playerVisionEnd;

        if (Physics.Raycast(playerVision, out playerVisionEnd, 10f))
        {
            targetItem = playerVisionEnd.collider.GetComponentInParent<Interactable>();
            if (targetItem != null)
            {
                //Debug.Log("looking at an interactable object");
            }
        }

        if (Input.GetKeyDown("f"))
        {
            Debug.Log("F pressed");
            if (targetItem != null)
            {
                Debug.Log("Interacting");
                targetItem.Interact(this);
            }
            else
            {
                Debug.Log("No Interactable Item in sight");
            }
        }

        if (Input.GetKey("q"))
        {
            //Debug.Log("Tossing Item");
            playerInventory.GetLeftHandItem(out Interactable itemRefL);
            playerInventory.GetRightHandItem(out Interactable itemRefR);
            if (Input.GetMouseButton(0) && itemRefL != null)
            {
                itemRefL.transform.parent = null;
                Rigidbody lhItemRB = itemRefL.GetComponent<Rigidbody>();
                lhItemRB.isKinematic = false;
                lhItemRB.AddForce(cameraController.playerCamera.transform.forward * tossForce);
                Debug.Log("Left Item tossed");
                playerInventory.RemoveLeftHandItem();
            }
            else if (Input.GetMouseButton(1) && itemRefR != null)
            {
                itemRefR.transform.parent = null;
                Rigidbody rhItemRB = itemRefR.GetComponent<Rigidbody>();
                rhItemRB.isKinematic = false;
                rhItemRB.AddForce(cameraController.playerCamera.transform.forward * tossForce);
                Debug.Log("Right Item tossed");
                playerInventory.RemoveRightHandItem();
            }
            /*if (playerInventory.rightHandItem != null)
            {
                playerInventory.rightHandItem.transform.parent = null;
                Rigidbody lhItemRB = playerInventory.rightHandItem.gameObject.GetComponent<Rigidbody>();
                lhItemRB.isKinematic = false;
                lhItemRB.AddForce(cameraController.playerCamera.transform.forward * tossForce);
                Debug.Log("Item Tossed: " + cameraController.playerCamera.transform.forward * tossForce);
                playerInventory.rightHandItem = null;
            }
            else
            {
                Debug.Log("no item toss");
            } */   
        }

        // for using with left mouse button
        if (Input.GetMouseButtonUp(0))
        {
            if (targetItem != null && targetItem.canPickup && !targetItem.twoHandItem)
            {
                playerInventory.AddLeftHandItem(targetItem, out bool addSuccess);
                if (addSuccess)
                {
                    targetItem.GetComponent<Rigidbody>().isKinematic = true;
                    targetItem.transform.parent = leftHandPos;
                    targetItem.transform.localPosition = new Vector3(0, 0, 0);
                    targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        // for using with right mouse button
        else if (Input.GetMouseButtonUp(1))
        {
            if (targetItem != null && targetItem.canPickup && !targetItem.twoHandItem)
            {
                playerInventory.AddRightHandItem(targetItem, out bool addSuccess);
                if (addSuccess)
                {
                    targetItem.GetComponent<Rigidbody>().isKinematic = true;
                    targetItem.transform.parent = rightHandPos;
                    targetItem.transform.localPosition = new Vector3(0, 0, 0);
                    targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }
        
        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
        {
            Debug.Log("try two hand");
            if (targetItem != null && targetItem.canPickup && targetItem.twoHandItem)
            {
                playerInventory.AddTwoHandItem(targetItem, out bool addSuccess);
                if (addSuccess)
                {
                    targetItem.GetComponent<Rigidbody>().isKinematic = true;
                    targetItem.transform.parent = twoHandPos;
                    targetItem.transform.localPosition = targetItem.transform.position  - targetItem.twoHandPos.position; // - targetItem.twoHandPos.localPosition;
                    targetItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    //Debug.Log(targetItem.twoHandPos.localPosition);
                }
            }
        }
    }
}
