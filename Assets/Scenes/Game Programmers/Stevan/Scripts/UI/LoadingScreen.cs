using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private float startingProgressValue; // Starting value of the slider ; in editor it's 30% - 0.3f
    [SerializeField] private float endProgressValue; // Ending value of slider ; in editor it's 100% - 1.0f
    [SerializeField] private float progressTime; // It's counting time for every change of the value ; in editor it's half a second - 0.5f
    [SerializeField] private float timeToFillBar; // The overall time it takes to load the slider from start to end ; in editor it's 2 seconds
    [SerializeField] private float sceneLoadTime; // Time it takes to load onto the new scene once the slider is full ; in editor it's 1 second
    [SerializeField] private float elapsedTime;
    [SerializeField] private Button continueButton; // Reference to the button
    private bool canLoadScene = false; // Add a flag to check if the scene can be loaded

    private void Start()
    {
        loadingScreen.SetActive(false);
        continueButton.interactable = false; // Disable the button initially
    }

    public async void LoadLevel(int levelToLoad)
    {
        StartCoroutine(LoadLevelEnum(levelToLoad));
        await Task.Delay(100);
    }

    IEnumerator LoadLevelEnum(int levelToLoad)
    {
        loadingScreen.SetActive(true);

        loadingSlider.value = startingProgressValue;

        while (elapsedTime < timeToFillBar)
        {

            if (elapsedTime >= progressTime && startingProgressValue < 0.7f)
            {
                startingProgressValue = 0.7f;
            }


            if (elapsedTime >= progressTime * 2 && startingProgressValue < 0.9f)
            {
                startingProgressValue = 0.9f;
            }

            loadingSlider.value = startingProgressValue;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        loadingSlider.value = endProgressValue;

        yield return new WaitForSeconds(sceneLoadTime);

        // Enable the button and set the flag to allow scene loading
        continueButton.interactable = true;
        canLoadScene = true;

        while (!canLoadScene)
        {
            yield return null;
        }

        //AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);

       // while (!operation.isdone)
       // {
       //     yield return null;
       // }
    }

    public void LoadSceneWithButton(int levelToLoad)
    {
        if (canLoadScene)
        {
            StartCoroutine(LoadSceneAsync(levelToLoad));
        }
    }

    IEnumerator LoadSceneAsync(int levelToLoad)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
