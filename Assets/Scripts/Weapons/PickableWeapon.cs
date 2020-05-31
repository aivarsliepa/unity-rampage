using UnityEngine;

public class PickableWeapon : MonoBehaviour
{
    public GunType gunType;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();
        if (player != null && player.PickWeapon(gunType))
        {
            Destroy(gameObject);
        }
    }
}
