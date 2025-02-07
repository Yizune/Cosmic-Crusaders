using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateTowardsTarget : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10f; //This is the sight range
    [SerializeField] private float rotationSpeed = 50;
    [SerializeField] private LayerMask includeLayer;
    
    void FixedUpdate()
    {
        RaycastHit hit;
        var colliders = Physics.OverlapSphere(transform.position, lookRadius, includeLayer);
        if (colliders.Length > 0)
        {
            FaceTargetPosition(colliders[0].transform);
        }
    }

    void FaceTargetPosition(Transform target)
    {
        var targetPosition = target.position;

        var plane = new Plane(transform.up, transform.position);
        var mappedTargetPosition = plane.ClosestPointOnPlane(targetPosition);

        Quaternion lookRot = Quaternion.LookRotation(mappedTargetPosition - transform.position, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotationSpeed);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
