using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotate : MonoBehaviour
{
    InputManager input;

    PlayerDash action;

    [SerializeField]
    Transform playerOne;

    [SerializeField]
    Transform playerTwo;


    [SerializeField]
    Rotate playerOneRotate;

    private bool p2IsMoving = false;


    [Header("Rotation Properties:")]

    [SerializeField]
    private float rotationSpeed = 150f;

    [SerializeField]
    private bool invert;

    private int invertDir = 1;


    [Header("Select Current Player:")]

    [SerializeField]
    private bool isPlayerOne;

    [SerializeField]
    private bool isPlayerTwo;


    private void Awake()
    {   
        input = new InputManager(GetComponent<PlayerInput>());

        action = FindObjectOfType<PlayerDash>();
    }

    private void FixedUpdate()
    {
        SelectPlayer();
        SetCurrentPlayerInput();
        RotatePlayer();
    }

    private void SetCurrentPlayerInput()
    {

        //Player 1
        if (isPlayerOne)
        {
            if (Gamepad.all.Count == 0)
            {
                input.playerInput.SwitchCurrentControlScheme(controlScheme: "PlayerOne", devices: Keyboard.current);
            }
            else if (Gamepad.all.Count >= 1)
            {
                InputDevice[] playerOneDevices = new InputDevice[] { Keyboard.current, Gamepad.all[0] };
                input.playerInput.SwitchCurrentControlScheme(controlScheme: "PlayerOne", devices: playerOneDevices);
            }

        }

        //Player 2
        else if (isPlayerTwo)
        {
            if (Gamepad.all.Count <= 1)
            {
                input.playerInput.SwitchCurrentControlScheme(controlScheme: "PlayerTwo", devices: Keyboard.current);
            }
            else if (Gamepad.all.Count == 2)
            {
                InputDevice[] playerTwoDevices = new InputDevice[] { Keyboard.current, Gamepad.all[1] };
                input.playerInput.SwitchCurrentControlScheme(controlScheme: "PlayerTwo", devices: playerTwoDevices);
            }
        }
    }

    private void SelectPlayer()
    {
        if (isPlayerOne && !isPlayerTwo)
        {
            isPlayerTwo = false;
        }
        else if (isPlayerOne && isPlayerTwo)
        {
            isPlayerTwo = false;
            isPlayerOne = false;
        }
        else if (!isPlayerOne && isPlayerTwo)
        {
            isPlayerOne = false;
            isPlayerTwo = true;
        }
    }

    private void RotatePlayer()
    {
        float time = Time.deltaTime;

        if (invert)
        {
            invertDir = -1;
        }
        else
        {
            invertDir = 1;
        }

        //Player 1
        if (input.RightButtonProperty && !action.isDashing && isPlayerOne)
        {
            playerOne.RotateAround(playerOne.position, playerOne.up * invertDir, rotationSpeed * time);
            
            if (!p2IsMoving)
            {
                playerTwo.RotateAround(playerTwo.position, playerTwo.up * -invertDir, rotationSpeed * time);
            }

        }
        else if (input.LeftButtonProperty && !action.isDashing && isPlayerOne)
        {
            playerOne.RotateAround(playerOne.position, playerOne.up * invertDir, -rotationSpeed * time);
            
            if (!p2IsMoving)
            {
                playerTwo.RotateAround(playerTwo.position, playerTwo.up * -invertDir, -rotationSpeed * time);
            }

        }


        //Player 2
        if (input.RightButtonProperty && isPlayerTwo)
        {
            p2IsMoving = true;
            playerOneRotate.p2IsMoving = true;

            playerTwo.RotateAround(playerTwo.position, playerTwo.up * invertDir, rotationSpeed * time);
        }
        else if (input.LeftButtonProperty && isPlayerTwo)
        {
            p2IsMoving = true;
            playerOneRotate.p2IsMoving = true;

            playerTwo.RotateAround(playerTwo.position, playerTwo.up * invertDir, -rotationSpeed * time);
        }
        else if (!input.RightButtonProperty && !input.LeftButtonProperty && isPlayerTwo)
        {
            p2IsMoving = false;
            playerOneRotate.p2IsMoving = false;
        }
    }

    private void OnEnable()
    {
        input.EnableInputs();
    }

    private void OnDisable()
    {
        input.DisableInputs();
    }
}
