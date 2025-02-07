using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerDash : MonoBehaviour
{
    InputManager input;

    MoveForward movement;

    [Header("Dashing Properties:")]

    [SerializeField] 
    public float dashSpeed;

    [SerializeField] 
    public float dashDuration;

    [SerializeField] 
    public float dashCooldown;

    public float defaultMoveSpeed;

    public bool isDashing;

    [HideInInspector]
    public bool hasDashed;


    [Header("Dashing UI Settings:")]

    [SerializeField] 
    private Image dashCooldownImage;

    [SerializeField] 
    private Text dashCooldownText;

    public UnityEvent onDash;

    private void Awake()
    {
        input = new InputManager(GetComponent<PlayerInput>());
        movement = GetComponent<MoveForward>();

        defaultMoveSpeed = movement.speed;

        isDashing = false;
        hasDashed = false;
    }

    private void Start()
    {
        dashCooldownImage.enabled = false;
        dashCooldownText.enabled = false;
    }

    private void FixedUpdate()
    {
        Dash();
    }

    private void Dash()
    {
        if (input.RightButtonProperty && input.LeftButtonProperty && !hasDashed)
        {
            isDashing = true;
            hasDashed = true;
            FindObjectOfType<AudioManager>().Play("Mover Dashing");
            movement.speed += dashSpeed;
            StartCoroutine(DashDuration());
            onDash.Invoke();
        }
    }

    private IEnumerator DashDuration()
    {
        dashCooldownImage.enabled = true;
        dashCooldownText.enabled = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        if (!isDashing && movement.speed != movement.defaultMoveSpeed)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Mover Dashing");
            movement.speed = defaultMoveSpeed;

            dashCooldownImage.enabled = true;
            float cooldownTime = dashCooldown;
            while (cooldownTime > 0)
            {
                dashCooldownImage.fillAmount = cooldownTime / dashCooldown;
                dashCooldownText.text = cooldownTime.ToString("F1");
                cooldownTime -= Time.deltaTime;
                yield return null;
            }

            dashCooldownImage.enabled = false;
            dashCooldownText.text = "";
        }

        yield return new WaitForSeconds(0f);
        hasDashed = false;
    }

    private void OnEnable()
    {
        input.EnableInputs();
    }

    private void OnDisable()
    {
        input.DisableInputs();
    }




    //Rasmus Code
    //private IEnumerator DashDuration()
    //{

    //    yield return new WaitForSeconds(dashDuration);
    //    isDashing = false;
    //    if (!isDashing && movement.speed != movement.defaultMoveSpeed)
    //    {
    //        movement.speed = movement.defaultMoveSpeed;
    //    }

    //    yield return new WaitForSeconds(dashDuration + dashCooldown);
    //    hasDashed = false;

    //}
}
