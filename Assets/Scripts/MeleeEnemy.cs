using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    public float attackRange = 1f;
    public int damage = 1;

    PlayerController player;
    Animator animator;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
        {
            Debug.Log(player.GetComponent<ObjectWithHealth>());
            player.GetComponent<ObjectWithHealth>().TakeDamage(damage);
        }
    }
}
