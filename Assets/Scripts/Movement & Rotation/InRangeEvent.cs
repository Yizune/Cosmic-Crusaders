using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class InRangeEvent : MonoBehaviour
{
    [SerializeField] LayerMask includeLayer;
    [SerializeField] float range;
    public UnityEvent onInRange;
    public UnityEvent onOutRange;
    bool isInRange = false;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, includeLayer);
        if (colliders.Length > 0)
        {
            onInRange.Invoke();
            isInRange = true;   
        }
        else if (isInRange)
        {
            onOutRange.Invoke();
            isInRange = false;
        } 
    }
}
