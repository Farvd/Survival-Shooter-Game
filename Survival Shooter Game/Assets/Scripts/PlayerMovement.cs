using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float jumpHeight = 2f;
    private float forwardInputValue;
    private float strafeInputValue;
    public float fallGravityMultiplier = 2f;
    private CharacterController characterController;
    private bool jumpInput;
    public float mouseSensitivity = 2f;
    public float CameraYaw;
    private UnityEngine.Camera firstPersonCam;


    private float terminalVelcity = 53f;
    private float verticalvelocity;

    void Awake()

    {
        characterController = GetComponent<CharacterController>();
        firstPersonCam = GetComponentInChildren<Camera>();


    }

    void Update()
    {
        if ((Keyboard.current.wKey.isPressed))
        {
            forwardInputValue = 1f; 
        }
        else if ((Keyboard.current.sKey.isPressed))
        {
            forwardInputValue = -1f;
        }
        else
        {
            forwardInputValue = 0f;
        }


        if ((Keyboard.current.dKey.isPressed))
        {
            strafeInputValue = 1f;
        }
        else if ((Keyboard.current.aKey.isPressed))
        {
            strafeInputValue = -1f;
        }
        else
        {
            strafeInputValue = 0f;
        }

        if ((Keyboard.current.spaceKey.isPressed)) 
        {     
            jumpInput = true;
        }
        else
        {
            jumpInput = false;
        }


        Movement();
        jumpAndGravity();
        CameraMovement();


        void Movement()
        {
            Vector3 direction = (transform.forward * forwardInputValue + transform.right * strafeInputValue).normalized * movementSpeed * Time.deltaTime;
            direction += Vector3.up * verticalvelocity * Time.deltaTime;
            characterController.Move(direction);
        }

        void jumpAndGravity()
        {
            if (characterController.isGrounded)
            {
                if (verticalvelocity < 0.0f)
                {
                    verticalvelocity = -2f;
                }
                if (jumpInput)
                {
                    verticalvelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                }
            }
            else
            {
                if (verticalvelocity < terminalVelcity)
                {
                    float gravityMultiplier = 1;
                    if (characterController.velocity.y < -1)
                    {
                        gravityMultiplier = fallGravityMultiplier;

                    }
                    verticalvelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
                }
            }
        }

        void CameraMovement()
        {

            mouseSensitivity = 2f;
            float TurnDegrees = Mouse.current.delta.x.ReadValue() * 0.1f * mouseSensitivity;
            float LookDegrees = Mouse.current.delta.y.ReadValue() * 0.1f * mouseSensitivity;
            firstPersonCam.transform.Rotate(-LookDegrees, 0, 0);
            transform.Rotate(0, TurnDegrees, 0);
        }

    }
}


