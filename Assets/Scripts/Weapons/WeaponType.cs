using UnityEngine;

public enum GunType
{
    HAND_GUN,
    MACHINE_GUN,
    FLAME_THROWER
}

public class WeaponType : MonoBehaviour
{
    public GunType gunType;
}
