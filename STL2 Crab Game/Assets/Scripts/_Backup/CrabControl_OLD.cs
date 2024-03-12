using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CrabControl_OLD : MonoBehaviour
{
    private Crab_Input input = null;

    // ---
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    // ---
    private Vector2 moveInput;
    private float jumpInput;

    private void Awake()
    {
        input = new Crab_Input();
    }


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }




    private void OnEnable()
    {
        input.Enable();

        // Arrows
        input.PlayerContol.Arrows.performed += OnPlayerMovementPerformed;
        input.PlayerContol.Arrows.canceled += OnPlayerMovementCancelled;
        // Left trigger (jump)
        input.PlayerContol.Jump.performed += OnPlayerJumpPerformed;
        input.PlayerContol.Jump.canceled += OnPlayerJumpCancelled;
    }
    private void OnDisable()
    {
        input.Disable();

        // Arrows
        input.PlayerContol.Arrows.performed -= OnPlayerMovementPerformed;
        input.PlayerContol.Arrows.canceled -= OnPlayerMovementCancelled;
        // Left trigger (jump)
        input.PlayerContol.Jump.performed -= OnPlayerJumpPerformed;
        input.PlayerContol.Jump.canceled -= OnPlayerJumpCancelled;
    }


    private void OnPlayerMovementPerformed(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    private void OnPlayerMovementCancelled(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    private void OnPlayerJumpPerformed(InputAction.CallbackContext value)
    {
        jumpInput = value.ReadValue<float>();
    }

    private void OnPlayerJumpCancelled(InputAction.CallbackContext value)
    {
        jumpInput = value.ReadValue<float>();

    }



    void Update()
    {
        MovePlayer_Basic();
    }

    void MovePlayer_Basic()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (jumpInput > 0 && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }



}
