using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject player;
    public float rotateYSpeed = 10f;
    public float rotateXSpeed = 10f;

    private void Awake()
    {
        // Add Checks here later
        
        //Cursor.visible = false;
    }

    private void Start()
    {
        // Run when the first frame updates
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float yawValue = player.transform.eulerAngles.y + mouseX * Time.deltaTime * rotateXSpeed;
        float pitchValue = playerCamera.transform.eulerAngles.x + (mouseY * Time.deltaTime * rotateYSpeed * -1);

        Quaternion newPlayerRot = Quaternion.Euler(0f, yawValue, 0f);
        Quaternion newCameraRot = Quaternion.Euler(pitchValue, 90f + yawValue, 0f);

        player.transform.rotation = newPlayerRot;
        playerCamera.transform.rotation = newCameraRot;

        
        /*if (gameFocus && moveInput)
        {
            // get mouse axises
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // assign new rotation value
            float yawValue = player.transform.eulerAngles.y + mouseX * Time.deltaTime * yawTurnPower;
            float pitchValue = playerCamera.transform.eulerAngles.x + (mouseY * Time.deltaTime * pitchTurnPower * -1);

            Quaternion newPlayerRot = Quaternion.Euler(0f, yawValue, 0f);
            Quaternion newCameraRot = Quaternion.Euler(pitchValue, 90f + yawValue, 0f);

            // assign new value to respected gameobject
            player.transform.rotation = newPlayerRot;
            playerCamera.transform.rotation = newCameraRot;
        }*/
    }
}
