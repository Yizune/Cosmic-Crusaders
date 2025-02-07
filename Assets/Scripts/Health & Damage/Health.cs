using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] public int maxHealth;
    [HideInInspector] public int currentHealth;
    public UnityEvent onTakeDamage, onDeath, onHealthReceived;
    void Awake ()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int changeAmount)
    {
        currentHealth += changeAmount;
        if (currentHealth + changeAmount >= maxHealth) 
        {
            currentHealth = maxHealth;
        }
        if (currentHealth + changeAmount < 0) 
        {
            currentHealth = 0;
        }
        if (currentHealth <= 0)
        {
            onDeath.Invoke();
        }
        if (changeAmount < 0)
        {
            onTakeDamage.Invoke();
        }
        if (changeAmount > 0)
        {
            onHealthReceived.Invoke();
        }
        // 'changeAmount' should be:
        // - Negative for taking damage (e.g., -10 for losing 10 HP)
        // - Positive for gaining health (e.g., 20 for gaining 20 HP)
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
