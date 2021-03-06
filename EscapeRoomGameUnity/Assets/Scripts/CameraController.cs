using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO::DO update camera controller script to reflect how camera works with inspection

public class CameraController : MonoBehaviour
{
    public Camera playerCamera;
    public Camera inspectCamera;
    public GameObject player;
    public CameraMode cameraMode = CameraMode.OutOfFocus;
    public float rotateYSpeed = 10f;
    public float rotateXSpeed = 10f;
    public float camCurX = 0f;
    public float camCurY = 0f;

    private void Awake()
    {
        // Add Checks here later
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Cursor.visible = false;
        //camCurX = player.transform.rotation.eulerAngles.y % 360;
    }

    private void Start()
    {
        // Run when the first frame updates
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // TO::DO check if this check should be here or player interaction
        // when player pressed the escape to go into the ui
        if (Input.GetKeyDown("escape") && cameraMode != CameraMode.InspectMode)
        {
            cameraMode = CameraMode.OutOfFocus;
        }

        // when player pressed middle mouse button to re focus and can't re focuse
        // while in inspect mode
        if (Input.GetMouseButtonDown(2) && cameraMode != CameraMode.InspectMode)
        {
            cameraMode = CameraMode.PlayMode;
        }
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        switch (cameraMode)
        { 
            case CameraMode.PlayMode:
                camCurX = (camCurX + (mouseX * Time.deltaTime * rotateXSpeed));
                camCurX = (camCurX + (camCurX < 0 ? 360 : 0)) % 360;
                camCurY += (mouseY * Time.deltaTime * rotateYSpeed * -1);
                break;
            case CameraMode.InspectMode:
                break;
            case CameraMode.OutOfFocus:
                break;
            default:
                break;
        }

        // TO::DO add const variable for min and max
        camCurY = Mathf.Clamp(camCurY, -80, 75);

        Quaternion newPlayerRot = Quaternion.Euler(0f, camCurX, 0f);
        Quaternion newCameraRot = Quaternion.Euler(camCurY, 90f, 0f);

        player.transform.localRotation = newPlayerRot;
        playerCamera.transform.localRotation = newCameraRot;
    }

    private void OnApplicationFocus(bool focus)
    {
        // TO::DO re add app focus later
        //cameraMode = focus ? CameraMode.PlayMode : CameraMode.OutOfFocus;
    }
    
    public void UpdateCameraMode(CameraMode newCameraMode)
    {
        cameraMode = newCameraMode;
    }

    public void PausePlayerCamera()
    {

    }

    public void ResumePlayerCamera()
    {

    }
}
