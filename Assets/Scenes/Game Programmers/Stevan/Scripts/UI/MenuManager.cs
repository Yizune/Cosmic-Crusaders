using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MenuManager : MonoBehaviour
{
    InputManager input;

    public GameObject pauseMenuUI;

    public static Action pauseGame;

    private static bool gameIsPaused;

    private void Awake()
    {
        input = new InputManager(GetComponent<PlayerInput>());
    }

    private void Start()
    {
        pauseGame += PauseGame;

        GetInputDevices();
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }
    
    public void PauseGame()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("Resume");
        gameIsPaused = false;
        Time.timeScale = 1f; // Unpause the game by setting time scale to 1 = sets the pace normally.
        pauseMenuUI.SetActive(false);
    }

    void Pause()
    {
        Debug.Log("Pause");
        gameIsPaused = true;
        Time.timeScale = 0f; // Pause the game by setting time scale to 0 = freezes it.
        pauseMenuUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    private void GetInputDevices()
    {
        if (Gamepad.all.Count == 0)
        {
            input.playerInput.SwitchCurrentControlScheme(controlScheme: "Pause", devices: Keyboard.current);
        }
        else if (Gamepad.all.Count == 1)
        {
            InputDevice[] oneGamePad = new InputDevice[] { Keyboard.current, Gamepad.all[0] };
            input.playerInput.SwitchCurrentControlScheme(controlScheme: "Pause", devices: oneGamePad);
        }
        else if (Gamepad.all.Count == 2)
        {
            InputDevice[] twoGamePads = new InputDevice[] { Keyboard.current, Gamepad.all[0], Gamepad.all[1] };
            input.playerInput.SwitchCurrentControlScheme(controlScheme: "Pause", devices: twoGamePads);
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
