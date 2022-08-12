using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    public float speed = 1f;

    //Camere Control
    private float xRotation = 0f;
    public float mouseSensitivity = 100f;

    //Jump and Gravity
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isGround;

    public Transform graundChecker;
    public float graundCheckerRadius;
    public LayerMask obstacleLayer;

    public float jumpHeight = 0.1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        //Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        //Check Character is grounded
        isGround = Physics.CheckSphere(graundChecker.position, graundCheckerRadius, obstacleLayer);

        Vector3 moveInput = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        Vector3 moveVelocity = moveInput * Time.deltaTime * speed;

        controller.Move(moveVelocity);

        //Camere Control
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity, 0);
        xRotation -= Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Jump and Gravity
        if (!isGround)
        {
            velocity.y += gravity * Time.deltaTime / 50;

        }
        else
        {
            velocity.y = -0.05f;

        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity / 100);
        }

        controller.Move(velocity);



    }
}

