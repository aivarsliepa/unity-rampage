using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float fireRate = 2.5f;
    public int damage = 5;

    private Weapon weapon;

    void Start()
    {
        weapon = new Weapon(bulletPrefab, bulletForce, fireRate, damage);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();
        if (player != null && player.PickWeapon(weapon))
        {
            Destroy(gameObject);
        }
    }
}
