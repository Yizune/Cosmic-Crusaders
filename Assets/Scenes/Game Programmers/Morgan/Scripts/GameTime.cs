using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameTime : MonoBehaviour
{
    [Tooltip("The textobject loads from a child object below. If you want another TextMeshPro text just remove the Text (TMP) child below and add your own instead")]
    [SerializeField] private TMP_Text gameTimeText;
    [Tooltip("the child lowest in the GameOverUI 1 Variant that is set to not active")]
    [SerializeField] private TMP_Text gameOverTitle;
    [SerializeField] private TMP_Text gameOverTextObject;
    private float gameTime, playerFinalTimeMinutes;
    [SerializeField] int textSize = 25;
    private int minutes, seconds, playerFinalTimeSec;
    private string playerFinalTimeString;
    private bool gameTimeIsRunning = false;
    
    void Start()
    {
        gameTime = 0f;
        // gameTimeText = GetComponentInChildren<TMP_Text>();
        gameTimeText.fontSize = textSize;
        StartGameTime();
        DisableGameTimeUI();
    }

    void Update()
    {
        UpdateGameTimeText();
    }

    void UpdateGameTimeText()
    {

        //gameTime += Time.deltaTime could be in update but then you don´t have all in one method.
        if (gameTimeIsRunning == true)
        {
            gameTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(gameTime / 60);
            seconds = Mathf.FloorToInt(gameTime % 60);

            if (minutes == 0)
            {
                gameTimeText.text = seconds + " sec";
            }
            else
            {
                gameTimeText.text = minutes + " min " + seconds + " sec";
            }
        }
        
    }
    
    public int PlayerEndTimeSeconds()
    {
        playerFinalTimeSec = seconds + (minutes * 60);
        return playerFinalTimeSec;
    }
    public float PlayerEndTimeMinutes()
    {
        playerFinalTimeMinutes = seconds/60f;
        Debug.Log("OBSERVE!  Ex: 2.33 minutes is 2 min and 1/3*minutes of seconds = 20sec");
        return playerFinalTimeMinutes;
    }
    public string PlayerEndTimeString()
    {
        string temp = seconds.ToString();
        string temp2 = minutes.ToString();
        playerFinalTimeString = temp2 + " minutes " + temp + " seconds";
        return playerFinalTimeString;
    }
    public float CurrentTimeSeconds()
    {
        playerFinalTimeSec = seconds + (minutes * 60);
        return playerFinalTimeSec;
    }
    public string CurrentTime()
    {
        string temp = seconds.ToString();
        string temp2 = minutes.ToString();
        playerFinalTimeString = temp2 + " minutes " + temp + " seconds";
        return playerFinalTimeString;
    }
    public void DisplayEndTimeUI()
    {
        string temp = seconds.ToString();
        string temp2 = minutes.ToString();
        playerFinalTimeString = temp2 + " minutes " + temp + " seconds";
        gameTimeText.text = temp2 + " minutes " + temp + " seconds";
        gameTimeIsRunning = false;
        EnableGameTimeUI();

    }
    public void ResetGameTime()
    { 
        gameTimeIsRunning = false;
        gameTime = 0;
        gameTimeText.text = gameTime.ToString() + " sec";
    }
    public void StartGameTime()
    {
        gameTimeIsRunning = true;
        UpdateGameTimeText();
    }
    public void PauseGameTime()
    {
        gameTimeIsRunning = false;
    }
    public void ContinueGameTime()
    {
        gameTimeIsRunning = true;
        UpdateGameTimeText();
    }
    public void DisableGameTimeUI()
    {
        gameTimeText.gameObject.SetActive(false);
        gameOverTitle.gameObject.SetActive(false);
    }
    public void EnableGameTimeUI()
    {
        gameTimeText.gameObject.SetActive(true);
        gameOverTitle.gameObject.SetActive(true);
    }

    public void GameOverText()
    {
        gameOverTextObject.gameObject.SetActive(true);
        gameOverTextObject.text = "Game Over your time is ";
        string temp = seconds.ToString();
        string temp2 = minutes.ToString();
        //playerFinalTimeString = "Game Over your time is "+ temp2 + " minutes " + temp + " seconds";
        gameOverTextObject.text = "Game finished! Your time is "+ temp2 + " minutes " + temp + " seconds";
        gameTimeIsRunning = false;
        gameTimeText.text = "";
        //gameTimeText.gameObject.SetActive(false);
    }

    public void DisplayGameOverText()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameOverText();
            Debug.Log("game over");
            gameTime = 0;
        }
    }
}

/*
 
  if (Input.GetKeyDown(KeyCode.W))
        {
            DisplayEndTimeUI();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGameTime();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGameTime();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGameTime();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ContinueGameTime();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            DisableGameTimeUI();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EnableGameTimeUI();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CurrentTime();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            CurrentTimeSeconds();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DisplayGameOverText();
        }
        
 */
