using UnityEngine;

public static class Rigidbody2DExtension
{
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius)
    {
        if (body == null) return;

        var dir = (body.transform.position - explosionPosition);
        var wearoff = 1 - (dir.magnitude / explosionRadius);
        body.AddForce(dir.normalized * (wearoff <= 0f ? 0f : explosionForce) * wearoff, ForceMode2D.Impulse);
    }
}
