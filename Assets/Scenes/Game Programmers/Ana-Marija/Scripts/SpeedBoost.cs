using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpeedBoost : MonoBehaviour
{
    private MoveForward movement;

    private PlayerDash dash;

    [SerializeField] 
    private int effectDurationTime; 
    
    [SerializeField] 
    private int newSpeed;

    [SerializeField]
    private GameObject speedUiEffect;

    [SerializeField]
    private Transform player;

    private float normalSpeed;
   // private int timeToDestroy = 3;
    private bool boostEffect;

    private void Awake()
    {
        dash = FindObjectOfType<PlayerDash>();
    }

    private void Start()
    {        
        boostEffect = false;
        for(var i = 0; i < transform.childCount; i++){
            if(!transform.GetChild(i).gameObject.activeSelf)transform.GetChild(i).gameObject.SetActive(true); 
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Speed Boost");
            movement = other.gameObject.GetComponentInChildren<MoveForward>();

            if (!dash.isDashing)
            {
                ActivateEffect();
            }
            else if (dash.isDashing)
            {
                StartCoroutine(WaitForDash());
            }
        }
    }

    private void ActivateEffect()
    {
        normalSpeed = movement.speed;
        if (boostEffect == false)
        {
            StartCoroutine("EffectDuration");
        }
    }

    IEnumerator EffectDuration()
    {
        for(var i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false); 
        }
        boostEffect = true; 
        movement.speed = newSpeed;
        // GameObject speedUiEffectRef = Instantiate(speedUiEffect, player.position, player.rotation, player);

        yield return new WaitForSeconds(effectDurationTime);
        //movement.speed = normalSpeed;
        movement.speed = movement.defaultMoveSpeed;
        FindObjectOfType<AudioManager>().StopPlaying("Speed Boost");
        // Destroy(speedUiEffectRef);
        Destroy(gameObject);
        
    }

    private IEnumerator WaitForDash()
    {
        yield return new WaitForSeconds(dash.dashDuration);
        
        ActivateEffect();
    }

}