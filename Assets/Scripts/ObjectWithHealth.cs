using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectWithHealth : MonoBehaviour
{
    public GameObject deathObject;
    public int maxHealth = 10;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int deltaHealth)
    {
        currentHealth += deltaHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
    }

    public void Heal(int health)
    {
        ChangeHealth(health);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        Instantiate(deathObject, transform.position, Quaternion.identity);
    }
}
