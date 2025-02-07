using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float heightDistance = 20;
    [SerializeField] private float forwardDistance = 0;
    [SerializeField] private float rotationAngle = 45;
    private Transform player;
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + player.up * heightDistance + player.forward * forwardDistance, Time.deltaTime * 35);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation * Quaternion.Euler(rotationAngle, 0, 0), Time.deltaTime * 25);
    }
}