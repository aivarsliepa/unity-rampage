using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 2.5f;

    bool canShoot = true;

    Crosshair crosshair;

    void Start()
    {
        crosshair = FindObjectOfType<Crosshair>();
    }

    void Update()
    {
        if (canShoot && Input.GetButton("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();

        Vector2 fireDirection = ((Vector2)firePoint.position).GetDirectionTo(crosshair.transform.position);
        bulletRigidbody.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1 / fireRate);

        canShoot = true;
    }
}
