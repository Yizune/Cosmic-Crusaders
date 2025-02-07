using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPower : MonoBehaviour
{
    [SerializeField]
    AutoShoot autoShoot;

    [SerializeField]
    private GameObject bulletPower;

    [SerializeField]
    private float fireRate_defaultBullet;

    [SerializeField]
    private float fireRate_bulletPower;

    private GameObject defaultBullet;

    private bool hasBulletPower;

    private void Awake()
    {
        defaultBullet = autoShoot.bullet;
        fireRate_defaultBullet = autoShoot.timeBetweenShots;
        hasBulletPower = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!hasBulletPower)
            {
                autoShoot.bullet = bulletPower;
                autoShoot.timeBetweenShots = fireRate_bulletPower;
                hasBulletPower = true;
                Destroy(gameObject);
            }
            else if (hasBulletPower)
            {
                autoShoot.bullet = defaultBullet;
                autoShoot.timeBetweenShots = fireRate_defaultBullet;
                hasBulletPower = false;
                Destroy(gameObject);

            }
        }
    }
}