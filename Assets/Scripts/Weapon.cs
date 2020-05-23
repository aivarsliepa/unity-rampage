using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class Weapon
{
    public GameObject bulletPrefab;
    public float bulletForce;
    public float fireRate;

    public int damage;
    public bool canShoot = true;

    private CollisionEvent collisionEvent;

    public Weapon(GameObject bulletPrefab, float bulletForce, float fireRate, int damage)
    {
        this.bulletForce = bulletForce;
        this.bulletPrefab = bulletPrefab;
        this.fireRate = fireRate;
        this.damage = damage;

        collisionEvent = new CollisionEvent();
        collisionEvent.AddListener(BulletCollisionHandler);
    }


    // private IEnumerator FireBullet(Transform firePoint)
    // {
    //     canShoot = false;

    //     GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //     bullet.GetComponent<Bullet>().RegisterCollisionEvent(collisionEvent);

    //     Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
    //     Vector2 fireDirection = ((Vector2)firePoint.position).GetDirectionTo(crosshair.transform.position);
    //     bulletRigidbody.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);

    //     yield return new WaitForSeconds(1 / fireRate);
    //     canShoot = true;
    // }

    // public IEnumerable FireAtTarget(GameObject bullet, Vector2 firePoint, Vector2 target)
    // {
    //     canShoot = false;

    //     bullet.GetComponent<Bullet>().RegisterCollisionEvent(collisionEvent);

    //     var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
    //     var fireDirection = firePoint.GetDirectionTo(target);
    //     bulletRigidbody.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);

    //     yield return new WaitForSeconds(1 / fireRate);
    //     canShoot = true;
    // }


    public async void FireAtTarget(GameObject bullet, Vector2 firePoint, Vector2 target)
    {
        canShoot = false;

        bullet.GetComponent<Bullet>().RegisterCollisionEvent(collisionEvent);

        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        var fireDirection = firePoint.GetDirectionTo(target);
        bulletRigidbody.AddForce(fireDirection * bulletForce, ForceMode2D.Impulse);

        await Task.Delay(TimeSpan.FromSeconds(1 / fireRate));
        canShoot = true;
    }

    private void BulletCollisionHandler(Collision2D collision)
    {
        collision.collider.GetComponent<ObjectWithHealth>()?.TakeDamage(damage);
    }
}