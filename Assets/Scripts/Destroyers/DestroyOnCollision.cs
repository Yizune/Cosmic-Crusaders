using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
  [SerializeField] private LayerMask includeLayer;
  void OnTriggerEnter(Collider other){
    if((( includeLayer & (1 << other.gameObject.layer)) != 0))
    Destroy(gameObject);
  }
}
