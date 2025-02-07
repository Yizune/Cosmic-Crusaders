using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MagneticItem : MonoBehaviour
{
  [SerializeField] private float range = 5;
  [SerializeField] private float force = 5;
  [SerializeField] private LayerMask includeLayer;
  void FixedUpdate() {
    if(Physics.CheckSphere(transform.position, range, includeLayer)){
      var target = Physics.OverlapSphere(transform.position, range, includeLayer)[0];
      GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * force, ForceMode.Impulse);
    }
  }
}
