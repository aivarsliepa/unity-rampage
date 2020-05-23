using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public float blastRadius = 2f;
    public float blastPower = 800f;

    public GameObject explosionPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GlobalConstants.LAYER_PLAYER_BULLET)
        {
            Vector2 explosionPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, blastRadius);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    AddExplosionForce(rb, blastPower, explosionPos, blastRadius);
                }

                EnemyController enemy = hit.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.Hit();
                }
            }

            Explode();
        }
    }


    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff, ForceMode2D.Impulse);
    }

    public static void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier)
    {
        var dir = (body.transform.position - explosionPosition);
        float wearoff = 1 - (dir.magnitude / explosionRadius);
        Vector3 baseForce = dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff;
        body.AddForce(baseForce);

        float upliftWearoff = 1 - upliftModifier / explosionRadius;
        Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
        body.AddForce(upliftForce);
    }

    public void Explode() {
        Destroy(gameObject);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
