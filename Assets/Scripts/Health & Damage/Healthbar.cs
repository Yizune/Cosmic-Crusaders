using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Health))]

public class Healthbar : MonoBehaviour
{
    public Slider healthBar;
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        healthBar.maxValue = health.currentHealth;
    }

    private void Update()
    {
        if (health.currentHealth != healthBar.value)
        {
            healthBar.value = health.currentHealth;
        }
    }
}
