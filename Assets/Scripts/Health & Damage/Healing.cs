using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Healing : MonoBehaviour
{
    Health playerHealth;
    [SerializeField] private int healAmount = 20;
    private Collider orbCollider;
    bool canHeal;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        orbCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (IsPlayer(collider))
        {
            FindObjectOfType<AudioManager>().StopPlaying("Health");
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                int remainingHeal = Mathf.Min(healAmount, playerHealth.maxHealth - playerHealth.currentHealth);
                playerHealth.ChangeHealth(remainingHeal);
                if (canHeal)
                {
                    Debug.Log("you are able to heal");
                }
                else if (!canHeal)
                {
                    Debug.Log("you are unable to heal");
                }
            }
            //Second debug doesn't work because trigger function won't work cuz am completely turning off colliders in Update
        }
    }

    private bool IsPlayer(Collider collider)
    {
        return collider.CompareTag("Player");
    }

    private void Update()
    {
        if (playerHealth.currentHealth == playerHealth.maxHealth)
        {
            orbCollider.enabled = false;
            canHeal = false;
        }
        else
        {
            orbCollider.enabled = true;
            canHeal = true;
        }
    }
}
