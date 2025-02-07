using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Android;

public class ColorChangeOnDamage : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer player;

    [SerializeField]
    private Color32 defaultColor;

    [SerializeField]
    private Color32 damageColor;


    [SerializeField]
    private float colorDuration;

    void Start() {
        ColorReset();
    }


    public void ChangeColor()
    {
        if(gameObject.activeSelf)StartCoroutine(ColorCooldown());
    }


    private IEnumerator ColorCooldown()
    {
        player.material.EnableKeyword("_EMISSION");
        player.material.SetColor("_EmissionColor", damageColor);
        yield return new WaitForSeconds(colorDuration);
        ColorReset();
    }



    private void ColorReset()
    {
        player.material.DisableKeyword("_EMISSION");
        player.material.SetColor("_EmissionColor", defaultColor);
    }

}
