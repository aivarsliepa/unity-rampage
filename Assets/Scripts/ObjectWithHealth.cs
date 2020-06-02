using UnityEngine;

public class ObjectWithHealth : MonoBehaviour
{
    public ParticleSystem hitEffect;
    public GameObject deathObject;
    public int maxHealth = 10;
    protected int currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void ChangeHealth(int deltaHealth)
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
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

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
