using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField]
    Mesh firstMesh;

    [SerializeField]
    Mesh secondMesh;

    [SerializeField]
    MeshFilter currentMesh;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float range = 5f;

    private void Update()
    {
        CheckDistance();
    }


    private void CheckDistance()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= range)
        {
            currentMesh.mesh = secondMesh;
        }
        else
        {
            currentMesh.mesh = firstMesh;
        }
    }

}
