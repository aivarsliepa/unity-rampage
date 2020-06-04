using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickableWeapon : MonoBehaviour
{
    public GunType gunType;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.GetComponent<PlayerController>();
        if (player != null && player.PickWeapon(gunType))
        {
            Destroy(gameObject);
        }
    }
}
