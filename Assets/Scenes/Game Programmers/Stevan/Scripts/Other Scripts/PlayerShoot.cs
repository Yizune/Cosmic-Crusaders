using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerShoot : MonoBehaviour
{
    public float lookRadius = 15f; //This is the sight range
    public float shootRadius = 12f; //This is the attack range
    public Transform target;

    //Attacking

    public Transform shootingPoint;
    public float timeBetweenAttacks;
    public float upwardForce;
    public float forwardForce;
    bool alreadyAttacked;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
                //Face the player
                FaceTarget();
            if (distance <= shootRadius)
            {
                //Attack the player
                AttackPlayer();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10);
    }

    void AttackPlayer()
    {

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(shootingPoint.forward * forwardForce, ForceMode.Impulse);
            rb.AddForce(shootingPoint.up * upwardForce, ForceMode.Impulse);
            //End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
