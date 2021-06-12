using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController charController;
    public Transform playerCameraPos;
    public bool isCrouching = false;
    public float playerHeight;
    public float cameraDiff = 0.36f;

    public float playerMoveSpeed = 10f;

    private void Awake()
    {
        // Add Checks here later
        playerHeight = charController.height;
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
        Vector3 moveDirection = new Vector3();

        moveDirection.x = -((forwardDir.x * rightMovement) + (rightDir.x * forwardMovement));
        moveDirection.z = -((forwardDir.z * rightMovement) + (rightDir.z * forwardMovement));
        moveDirection *= playerMoveSpeed * Time.deltaTime;

        // move the player
        charController.Move(moveDirection);

        if (Input.GetKeyDown("left ctrl"))
        {
            if (isCrouching)
            {
                // TO::DO need to add a check to see if the player can stand up
                /*
                Collider[] capTest = Physics.OverlapSphere(transform.position + new Vector3(0, 1.5f, 0), 0.5f);
                if (capTest.Length > 0)
                {
                    Debug.Log("There is something in the way");
                    foreach(Collider cap in capTest)
                    {
                        Debug.Log(cap.name);
                    }
                }
                else
                {
                    Debug.Log("Can Stand up");
                }*/

                charController.height = playerHeight;
                charController.center = new Vector3(0, playerHeight / 2, 0);
                playerCameraPos.localPosition = new Vector3(0, playerHeight - cameraDiff, 0);
                isCrouching = !isCrouching;
            }
            else
            {
                charController.height = playerHeight/2;
                charController.center = new Vector3(0, playerHeight / 4, 0);
                playerCameraPos.localPosition = new Vector3(0, playerHeight/2 - cameraDiff, 0);
                isCrouching = !isCrouching;
            }
        }
    }
}
