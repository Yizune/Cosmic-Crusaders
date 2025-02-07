using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMoveDirection : MonoBehaviour
{
    Transform planet;

    [Header("Player Properties:")]

    public float currentSpeed = 5f;
    public float defaultSpeed = 5f;

    private void Awake()
    {
        planet = GameObject.FindWithTag("Planet").transform;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        transform.RotateAround(planet.position, transform.right, currentSpeed * Time.deltaTime);
    }
}
