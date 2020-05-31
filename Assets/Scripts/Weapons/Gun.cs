using UnityEngine;
using System.Threading.Tasks;
using System;

//public float bulletForce = 20f;
//public float fireRate = 2.5f;
//public int damage = 5;

[Serializable]
public struct GunStats
{
    public GameObject bulletPrefab;
    public float bulletForce;
    public float fireRate;
    public int damage;
    public BulletLayer bulletLayer;
}

public class Gun : WeaponBase
{
    public GunStats Stats;
    public Transform firePoint;

    private bool canShoot;
    private CollisionEvent collisionEvent;

    public void Awake()
    {
        canShoot = true;
        collisionEvent = new CollisionEvent();
        collisionEvent.AddListener(BulletCollisionHandler);
    }

    public override void FireAt(Vector2 target)
    {
        if (canShoot)
        {
            FireBullet(target);
        }
    }

    private async void FireBullet(Vector2 target)
    {
        canShoot = false;

        GameObject bullet = Instantiate(Stats.bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.layer = (int)Stats.bulletLayer;
        bullet.GetComponent<Bullet>().RegisterCollisionEvent(collisionEvent);

        var fireDirection = ((Vector2)firePoint.position).GetDirectionTo(target);
        var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.AddForce(fireDirection * Stats.bulletForce, ForceMode2D.Impulse);

        await Task.Delay(TimeSpan.FromSeconds(1 / Stats.fireRate));
        canShoot = true;
    }

    private void BulletCollisionHandler(Collision2D collision)
    {
        collision.collider.GetComponent<ObjectWithHealth>()?.TakeDamage(Stats.damage);
    }
}