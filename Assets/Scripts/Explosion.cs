﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float blastRadius = 2f;
    public float blastPower = 800f;
    public int damage = 20;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, blastRadius);
        foreach (Collider2D hit in colliders)
        {
            hit.GetComponent<Rigidbody2D>()?.AddExplosionForce(blastPower, explosionPos, blastRadius);
            hit.GetComponent<ObjectWithHealth>()?.TakeDamage(damage);
        }
    }
}