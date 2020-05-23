using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    PlayerController player;
    float maxSpeed = 1f;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate()
    {
        transform.rotation = Utils.GetRotationAngle(transform.position, player.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.fixedDeltaTime);
    }
}
