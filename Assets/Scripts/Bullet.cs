using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    CollisionEvent collisionEvent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        collisionEvent.Invoke(collision);
    }

    public void Fire()
    {

    }

    public void RegisterCollisionEvent(CollisionEvent collisionEvent) {
        this.collisionEvent = collisionEvent;
    }
}
