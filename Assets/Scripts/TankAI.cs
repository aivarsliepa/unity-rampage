using UnityEngine;

[RequireComponent(typeof(Gun))]
public class TankAI : MonoBehaviour
{
    private Gun gun;
    private Transform shootingTarget;

    void Start()
    {
        shootingTarget = FindObjectOfType<PlayerController>().transform;
        gun = GetComponent<Gun>();
    }

    void Update()
    {
        gun.FireAt(shootingTarget.position);
    }
}
