using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateOnCollision : MonoBehaviour
{

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float distance;

    private bool isColliding;


    private void FixedUpdate()
    {
        StartRotate();
    }

    private void StartRotate()
    {
        isColliding = Physics.Raycast(transform.position, transform.forward, distance, layerMask) ||
            Physics.Raycast(transform.position, transform.forward + transform.right, distance / 2, layerMask) ||
            Physics.Raycast(transform.position, transform.forward - transform.right, distance / 2, layerMask);

        if (isColliding){
            transform.RotateAround(transform.position, transform.up, rotationSpeed * Time.deltaTime);
        }
    }

}
