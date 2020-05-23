using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bloodStein;
    PlayerController player;
    float maxSpeed = 1f;

    void Start() {
        player = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate() {
        transform.rotation = Utils.GetRotationAngle(transform.position, player.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.fixedDeltaTime);
    }

    public void Hit() {
        Destroy(gameObject);
        Instantiate(bloodStein, transform.position, Quaternion.identity);
    }
}
