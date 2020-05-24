using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Crosshair crosshair;
    public Transform firePoint;
    public Weapon weapon;

    public int bulletLayer = 0;

    void Start()
    {
        crosshair = FindObjectOfType<Crosshair>();
    }

    public void Shoot()
    {
        if (weapon != null && weapon.canShoot)
        {
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(weapon.bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = bulletLayer;
        weapon.FireAtTarget(bullet, firePoint.position, crosshair.transform.position);
    }
}
