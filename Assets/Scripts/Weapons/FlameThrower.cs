using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : WeaponBase
{
    public ParticleSystem flame;

    public override void FireAt(Vector2 target)
    {
        if (!flame.isPlaying)
        {
            flame.Play();
        }
    }
}
