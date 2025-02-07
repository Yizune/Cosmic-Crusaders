using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    Transform planet;
    
    [SerializeField]
    private LayerMask collisionLayer;

    public float speed;

    public float defaultMoveSpeed;

    private bool canMove;
    
    private float distance = 1f;

    private void Awake()
    {
        planet = GameObject.FindWithTag("Planet").transform;
        if(defaultMoveSpeed == 0)defaultMoveSpeed = speed;
        if(speed != defaultMoveSpeed) speed = defaultMoveSpeed;
    }
    

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        canMove = !Physics.Raycast(transform.position, transform.forward, distance, collisionLayer) &&
            !Physics.Raycast(transform.position, transform.forward + transform.right, distance / 2, collisionLayer) &&
            !Physics.Raycast(transform.position, transform.forward - transform.right, distance / 2, collisionLayer);

        if (canMove)
        {
            
            transform.RotateAround(planet.position, transform.right, speed * Time.deltaTime);
        }
    }

    public void StopMovement()
    {
        speed = 0f;
    }

    public void StartMovement()
    {
        speed = defaultMoveSpeed;
        
    }
}
