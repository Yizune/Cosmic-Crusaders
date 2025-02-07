using System.Collections;
using UnityEngine;

public class ShieldBoost : MonoBehaviour
{
    [SerializeField] 
    private int effectDurationTime;
    
    [SerializeField] 
    GameObject shieldObject;
    
    private SphereCollider mySphereCollider;
    private Transform player;
    [SerializeField] private bool boostEffect=false;
    private GameObject alreadyExistingShield;

    void Start()
    {
        mySphereCollider = GetComponent<SphereCollider>();
        for(var i = 0; i < transform.childCount; i++){
            if(!transform.GetChild(i).gameObject.activeSelf)transform.GetChild(i).gameObject.SetActive(true); 
        }
        if(!mySphereCollider.enabled)mySphereCollider.enabled = true;
    }
/*    private void Update()
    {
        if (alreadyExistingShield != null)
        {
            boostEffect = true;
        }
        else if (alreadyExistingShield == null && boostEffect)
        {
            boostEffect = false;
        }
    }*/
    private void OnTriggerEnter(Collider other)
    {
        alreadyExistingShield = GameObject.FindWithTag("Shield");
        if (alreadyExistingShield != null)
        {
            boostEffect = true;
        }
        else if (alreadyExistingShield == null && boostEffect)
        {
            boostEffect = false;
        }
        if (boostEffect == false)
        {
            FindObjectOfType<AudioManager>().Play("Shield Boost");
            if (other.gameObject.CompareTag("Player"))
            {
                player = other.gameObject.transform;
                StartCoroutine("EffectDuration");
            }
        }
    }
    IEnumerator EffectDuration()
    {
        for(var i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false); 
        }
        mySphereCollider.enabled = false;
        var shieldObjectRef = Instantiate(shieldObject, player.position, player.rotation, player);
        yield return new WaitForSeconds(effectDurationTime);
        FindObjectOfType<AudioManager>().StopPlaying("Shield Boost");
        Destroy(shieldObjectRef);
        Destroy(gameObject);
        
    }
}