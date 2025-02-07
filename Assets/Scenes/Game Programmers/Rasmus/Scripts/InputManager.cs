using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public PlayerInput playerInput;

    public InputManager(PlayerInput _player)
    {
        playerInput = _player;
    }


    private bool rightButton;
    public bool RightButtonProperty
    {
        get { return rightButton; }
        private set { rightButton = value; }
    }

    private bool leftButton;
    public bool LeftButtonProperty
    {
        get { return leftButton; }
        private set { leftButton = value; }
    }

    public Vector2 navigate;

    private void GetRightButton(InputAction.CallbackContext ctx)
    {
        rightButton = ctx.ReadValueAsButton();
    }

    private void StopRightButton(InputAction.CallbackContext ctx)
    {
        rightButton = false;
    }

    private void GetLeftButton(InputAction.CallbackContext ctx)
    {
        leftButton = ctx.ReadValueAsButton();
    }

    private void StopLeftButton(InputAction.CallbackContext ctx)
    {
        leftButton = false;
    }

    private void GetNavigate(InputAction.CallbackContext ctx)
    {
        navigate = ctx.ReadValue<Vector2>();
    }
    
    private void GetPauseButton(InputAction.CallbackContext ctx)
    {
        MenuManager.pauseGame?.Invoke();
    }

    public void EnableInputs()
    {
        playerInput.actions.Enable();

        playerInput.actions.FindAction("RightTurn").performed += GetRightButton;
        playerInput.actions.FindAction("RightTurn").canceled += StopRightButton;

        playerInput.actions.FindAction("LeftTurn").performed += GetLeftButton;
        playerInput.actions.FindAction("LeftTurn").canceled += StopLeftButton;

        playerInput.actions.FindAction("Navigate").performed += GetNavigate;
        playerInput.actions.FindAction("Navigate").canceled += GetNavigate;

        playerInput.actions.FindAction("Pause").performed += GetPauseButton;


    }

    public void DisableInputs()
    {
        playerInput.actions.Disable();

        playerInput.actions.FindAction("RightTurn").performed -= GetRightButton;
        playerInput.actions.FindAction("RightTurn").canceled -= StopRightButton;


        playerInput.actions.FindAction("LeftTurn").performed -= GetLeftButton;
        playerInput.actions.FindAction("LeftTurn").canceled -= StopLeftButton;

        
        playerInput.actions.FindAction("Pause").performed -= GetPauseButton;

    }
}
