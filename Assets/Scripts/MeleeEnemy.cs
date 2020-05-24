using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    public float attackRange = 1f;
    public int damage = 1;
    public float attackRate = 1f;
    private bool canAttack = true;

    PlayerController player;
    Animator animator;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canAttack && Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() {
        canAttack = false;

        animator.SetTrigger("Attack");
        player.GetComponent<ObjectWithHealth>().TakeDamage(damage);

        yield return new WaitForSeconds(1 / attackRate);

        canAttack = true;
    }
}
