using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{
    InputManager input;

    [SerializeField]
    private GameObject specialBullet;

    [SerializeField]
    private float coolDownTime;

    private bool canShoot;
    public UnityEvent onShoot;
    private void Awake()
    {
        input = new InputManager(GetComponent<PlayerInput>());

        canShoot = true;
    }


    private void Update()
    {
        SpecialAttack();
    }

    private void SpecialAttack()
    {
        if (input.RightButtonProperty && input.LeftButtonProperty && canShoot)
        {
            onShoot.Invoke();
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canShoot = false;
        
        Instantiate(specialBullet, transform.position + transform.forward * transform.lossyScale.z, transform.rotation);

        yield return new WaitForSeconds(coolDownTime);
        canShoot = true;
    }


    private void OnEnable()
    {
        input.EnableInputs();
    }

    private void OnDisable()
    {
        input.DisableInputs();
    }
}
