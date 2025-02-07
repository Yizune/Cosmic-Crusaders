using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticPlayerStartPosition : MonoBehaviour
{
    private Transform planet;

    public float offset;

    private void Start()
    {
        planet = GameObject.FindWithTag("Planet").transform;
        float playerHeight = transform.lossyScale.y / 2;
        float planetRadius = planet.lossyScale.y / 2;
        offset = planetRadius + playerHeight;
        transform.position = new Vector3(0, offset, 0);
    }
}
