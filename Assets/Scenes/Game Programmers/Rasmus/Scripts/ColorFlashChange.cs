using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlashChange : MonoBehaviour
{
    [Header("Object Renderer:")]

    [SerializeField]
    Renderer meshRenderer;

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



    private void Awake()
    {
        isEmission = false;

        lightEmission = false;
        darkEmission = false;
    }

    private void Update()
    {
        SetColor();
        EmissionLighting();
    }


    private void SetColor()
    {
        if (isEmission)
        {
            emissionColor = currentColor;
            meshRenderer.material.color = defaultColor;

            meshRenderer.material.EnableKeyword("_EMISSION");
            meshRenderer.material.SetColor("_EmissionColor", emissionColor);

        }
        else if (!isEmission)
        {
            meshRenderer.material.color = currentColor;
            meshRenderer.material.DisableKeyword("_EMISSION");
        }

        if (!currentColor.Equals(defaultColor))
        {
            lastSelectedColor = currentColor;
            StartCoroutine(ColorCooldown());
        }
    }

    private void ColorReset()
    {
        currentColor = defaultColor;
        lastSelectedColor = defaultColor;
        emissionColor = defaultColor;
    }

    private IEnumerator ColorCooldown()
    {
        yield return new WaitForSeconds(colorDuration);
        ColorReset();
    }


    private void EmissionLighting()
    {
        if (lightEmission && !darkEmission)
        {
            defaultColor = Color.white;
        }
        else if (darkEmission && !lightEmission)
        {
            defaultColor = Color.black;
        }
        else if ((darkEmission && lightEmission) || (!darkEmission && !lightEmission))
        {
            defaultColor = Color.white;
        }
    }

}
