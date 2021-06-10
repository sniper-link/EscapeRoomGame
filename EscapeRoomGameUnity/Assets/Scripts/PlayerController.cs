using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController charController;
    
    public float playerMoveSpeed = 10f;

    private void Awake()
    {
        // Add Checks here later
    }

    private void Start()
    {
        // Run when the first frame updates
    }

    private void Update()
    {
        // get player axis controls
        float forwardMovement = Input.GetAxis("Vertical");
        float rightMovement = Input.GetAxis("Horizontal");

        // get player current facing direction
        Vector3 forwardDir = this.transform.forward;
        Vector3 rightDir = this.transform.right * -1;

        // construct new move direction
        //Vector3 moveDirection = new Vector3(forwardMovement * forwardDir.x * Time.deltaTime, 0f, rightMovement * rightDir.z * Time.deltaTime);
        Vector3 moveDirection = new Vector3();

        moveDirection.x = -((forwardDir.x * rightMovement) + (rightDir.x * forwardMovement));
        moveDirection.z = -((forwardDir.z * rightMovement) + (rightDir.z * forwardMovement));
        moveDirection *= playerMoveSpeed * Time.deltaTime;

        // move the player
        charController.Move(moveDirection);

        

        /*if (gameFocus && moveInput)
        {

            GameObject lookAtItem = null;
            Ray playerVision = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit playerVisionEnd;

            if (Physics.Raycast(playerVision, out playerVisionEnd, 10f))
            {
                if (playerVisionEnd.collider.tag == "InteractableItem")
                {
                    canInteract = true;
                    lookAtItem = playerVisionEnd.collider.gameObject;
                }
                else if (playerVisionEnd.collider.tag == "Door")
                {
                    canInteract = true;
                    lookAtItem = playerVisionEnd.collider.gameObject;
                }
                else
                {
                    canInteract = false;
                }
                //Debug.Log(playerVisionEnd.collider.name);
            }

            if (canInteract && Input.GetKeyDown("f") && lookAtItem != null)
            {
                if (lookAtItem.tag == "InteractableItem")
                {
                    InteractableItem curItem = lookAtItem.GetComponent<InteractableItem>();

                    m_playerInventory.AddToInventory(curItem);
                    if (rightHandPos.transform.childCount == 0)
                    {
                        //lookAtItem.transform.position = new Vector3(0f, 0f, 0f);
                        lookAtItem.transform.SetParent(rightHandPos.transform);

                        lookAtItem.transform.rotation = new Quaternion();
                        lookAtItem.transform.localPosition = new Vector3(0f, 0f, 0f);
                        objectInHand = lookAtItem;
                    }
                    else
                    {
                        curItem.Interact();
                    }
                }
                else if (lookAtItem.tag == "Door")
                {
                    lookAtItem.GetComponent<Door>().Interact(this.gameObject);
                }
            }

        }

        if (Input.GetKeyDown("escape"))
        {
            if (m_playerUI.inUse)
            {
                m_playerUI.CloseKeypad();
            }
            else if (!m_playerUI.inUse)
            {
                gameFocus = !gameFocus;
            }

            //Cursor.visible = gameFocus;
        }*/
    }

    /*
    private void OnApplicationFocus(bool hasFocus)
    {
        gameFocus = hasFocus;
    }

    private void OnApplicationPause(bool gamePause)
    {
        gameFocus = !gamePause;
    }

    private void OnEnable()
    {
        gameFocus = true;
    }

    private void OnMouseDown()
    {
        gameFocus = true;
        //Cursor.visible = false;
    }
    */
}
