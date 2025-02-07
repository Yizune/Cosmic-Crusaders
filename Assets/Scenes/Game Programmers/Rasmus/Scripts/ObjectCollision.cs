using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectCollision : MonoBehaviour
{
    [SerializeField] private LayerMask includeLayer;
    public UnityEvent onWallBreak;

    private void OnTriggerEnter(Collider other)
    {
        if((( includeLayer & (1 << other.gameObject.layer)) != 0))
        {
            onWallBreak.Invoke();
        }
    }
}
