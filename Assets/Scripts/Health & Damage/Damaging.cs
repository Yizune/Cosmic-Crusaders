using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Damaging : MonoBehaviour
{
    [SerializeField] private int damageAmount = -10;
    [SerializeField] private LayerMask includeLayer;
    public UnityEvent onDoDamage;
    private void OnTriggerEnter(Collider collider)
    {
        if((( includeLayer & (1 << collider.gameObject.layer)) != 0))
        {
            if (collider.TryGetComponent<Health>(out Health health))
            {
                health.ChangeHealth(damageAmount);
                onDoDamage.Invoke();
            }
        }
    }
}
