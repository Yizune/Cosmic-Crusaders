using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class AutoShoot : MonoBehaviour
{
   [SerializeField] 
    public GameObject bullet;

    [SerializeField] 
    public float timeBetweenShots;

    [SerializeField] 
    private bool shootFromStart = true; 
    private bool isShooting = true;

    [SerializeField] private Vector3 offset;

    private bool readyToShoot = true;
    public UnityEvent onShoot;
    void Start() {
        if(shootFromStart) isShooting = true;
        else isShooting = false;
    }
    public void StartShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
    }
    void FixedUpdate(){
        if(isShooting && readyToShoot)StartCoroutine(Shoot());
    }
    IEnumerator Shoot()
    {
        onShoot.Invoke();
        readyToShoot = false;
        Instantiate(bullet, transform.position + transform.forward * transform.lossyScale.z + offset, transform.rotation);
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }


}


