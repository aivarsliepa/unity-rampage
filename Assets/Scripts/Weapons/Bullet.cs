using UnityEngine;

// For now just a projectile, no logic attached to it, emits when hits
public class Bullet : MonoBehaviour
{
    private CollisionEvent collisionEvent;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        collisionEvent.Invoke(collision);
    }

    public void RegisterCollisionEvent(CollisionEvent collisionEvent) {
        this.collisionEvent = collisionEvent;
    }
}
