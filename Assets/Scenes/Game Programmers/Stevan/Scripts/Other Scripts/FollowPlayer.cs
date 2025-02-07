using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
     public float lookRadius = 10f; //This is the sight range
     public float shootRadius = 8f; //This is the attack range
     public Transform target;
     public NavMeshAgent agent; 
     public float speed;

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
         target = PlayerManager.instance.player.transform;
         agent = GetComponent<NavMeshAgent>();
     }

     // Update is called once per frame
     void Update()
     {
         float distance = Vector3.Distance(target.position, transform.position);

         if (distance <= lookRadius)
         {
            //Traveling towards the player
            ChasePlayer();

             if(distance <= agent.stoppingDistance)
             {
                 //Face the player
                 FaceTarget();
             }
             if(distance <= shootRadius)
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

     void ChasePlayer()
     {
        agent.SetDestination(target.position);
        agent.speed = speed;
     }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position); //- use if we want enemy to shoot while standing only, and not move constantly and shoot

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

    void Patroling()
     {
        
     }  

     private void OnDrawGizmosSelected()
     {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
