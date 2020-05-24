using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    Shooter shooter;
    Weapon weapon;

    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 2.5f;
    public int damage = 5;

    void Start()
    {
        shooter = GetComponent<Shooter>();
        shooter.bulletLayer = GlobalConstants.LAYER_ENEMY_BULLET;
        shooter.weapon = new Weapon(bulletPrefab, bulletForce, fireRate, damage);
    }

    void Update()
    {
        shooter.Shoot();
    }
}
