
using UnityEngine;

public abstract class WeaponBase : WeaponType
{
    public abstract void FireAt(Vector2 target);
    public virtual void StopFiring() { }
}
