using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFadeChange : MonoBehaviour
{
    [Header("Object Renderer:")]

    [SerializeField]
    MeshRenderer meshRenderer;

    [Header("Color Settings:")]

    [SerializeField]
    private Color32 currentColor;

    [SerializeField]
    private Color32 defaultColor;

    [SerializeField]
    private Color32 lastSelectedColor;

    [SerializeField]
    private Color32 emissionColor;


    [Space(10)]


    [SerializeField]
    private float colorDuration;


    [Space(10)]


    [SerializeField]
    private bool isEmission;

    [SerializeField]
    private bool lightEmission;

    [SerializeField]
    private bool darkEmission;

    private bool isChanging;
    private bool timerOne;
    private bool timerTwo;
    private float timeOne = 0f;
    private float timeTwo = 0f;


    private void Awake()
    {
        isEmission = false;

        lightEmission = false;
        darkEmission = false;

        timerOne = false;
        timerTwo = false;
        isChanging = false;
    }

    private void Update()
    {
        EmissionLighting();
        
        UpdateSlowColor();
        CheckTime();
    }

    private void EmissionLighting()
    {
        if (lightEmission && !darkEmission)
        {
            defaultColor = Color.white;
        }
        else if(darkEmission && !lightEmission)
        {
            defaultColor = Color.black;
        }
        else if((darkEmission && lightEmission) || (!darkEmission && !lightEmission))
        {
            defaultColor = Color.white;
        }
    }

    private void CheckTime()
    {
        if(timerOne)
        {
            timeOne += Time.deltaTime;
        }
        if(timerTwo)
        {
            timeTwo += Time.deltaTime;
        }
    }

    private void UpdateSlowColor()
    {
        if (isChanging)
        {
            SlowColorReset();
        }
        else if (!isChanging)
        {
            SlowColorChange();
        }

    }

    private void SlowColorChange()
    {
        if (isEmission)
        {
            emissionColor = currentColor;
            
            meshRenderer.material.color = Color32.Lerp(defaultColor, emissionColor, timeOne * 0.5f);
            
            meshRenderer.material.color = defaultColor;
            
            meshRenderer.material.EnableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", emissionColor);

        }
        else if (!isEmission)
        {
            meshRenderer.material.color = Color32.Lerp(defaultColor, currentColor, timeOne * 0.5f);
            meshRenderer.material.DisableKeyword("_EMISSION");
        }


        if (!currentColor.Equals(lastSelectedColor))
        {
            timerOne = true;
            lastSelectedColor = currentColor;

            StartCoroutine(SlowColorCooldown());
        }
    }

    private void SlowColorReset()
    {
        if (isEmission)
        {
            emissionColor = currentColor;

            meshRenderer.material.color = Color32.Lerp(emissionColor, defaultColor, timeTwo * 0.5f);

            meshRenderer.material.color = defaultColor;

            meshRenderer.material.EnableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", emissionColor);

        }
        else if (!isEmission)
        {
            meshRenderer.material.color = Color32.Lerp(currentColor, defaultColor, timeTwo * 0.5f);
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        if (!currentColor.Equals(defaultColor))
        {
            timerTwo = true;
            lastSelectedColor = currentColor;

            if (timeTwo > 3f)
            {
                isChanging = false;
                timeOne = 0f;
                timeTwo = 0f;
                timerOne = false;
                timerTwo = false;
                currentColor = defaultColor;
                lastSelectedColor = defaultColor;
                emissionColor = defaultColor;

            }
        }
    }

    private IEnumerator SlowColorCooldown()
    {
        //yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(colorDuration);
        isChanging = true;
        SlowColorReset();

    } 
}
