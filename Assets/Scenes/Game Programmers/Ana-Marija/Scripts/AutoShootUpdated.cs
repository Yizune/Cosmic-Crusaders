using System.Collections;
using UnityEngine;
public class AutoShootUpdated : MonoBehaviour
{
    [SerializeField] public GameObject bullet;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private bool isShooting = true;
    private bool readyToShoot = true;
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
    IEnumerator Shoot(){
        readyToShoot = false;
        Instantiate(bullet, transform.position + transform.forward * transform.lossyScale.z, transform.rotation);
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }
}
