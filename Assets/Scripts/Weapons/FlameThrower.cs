using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : WeaponBase
{
    public ParticleSystem flame;

    public int fireDamage = 1;
    public float burnRate = 10f;

    private bool isFiring = false;

    private List<ObjectWithHealth> enemiesInRadius;

    public void Awake()
    {
        enemiesInRadius = new List<ObjectWithHealth>();
        flame.Stop();
    }

    public override void FireAt(Vector2 target)
    {
        if (!isFiring)
        {
            Debug.Log("START!");
            flame.Play();
            StartCoroutine(BurnThePlace());
        }

        isFiring = true;
    }

    public override void StopFiring()
    {
        Debug.Log("STOP!");
        flame.Stop();
        isFiring = false;
    }

    private IEnumerator BurnThePlace()
    {
        BurnEnemiesInRadius();

        yield return new WaitForSeconds(1 / burnRate);

        if (isFiring)
        {
            StartCoroutine(BurnThePlace());
        }
    }


    private void BurnEnemiesInRadius()
    {
        // have to use for loop, because Destroy() while iterating cause exception
        for (int i = 0; i < enemiesInRadius.Count; i++)
        {
            enemiesInRadius[i].TakeDamage(fireDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<ObjectWithHealth>();
        if (enemy != null)
        {
            enemiesInRadius.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<ObjectWithHealth>();
        if (enemy != null)
        {
            enemiesInRadius.Remove(enemy);
        }
    }
}
