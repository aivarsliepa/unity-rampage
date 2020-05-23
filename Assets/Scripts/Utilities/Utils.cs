using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //
    // Summary:
    //     Returns rotation angle that can be used to rotate sprite
    //      so that it feels like it is looking at something (top - down)
    //
    public static Quaternion GetRotationAngle(Vector2 currentPosition, Vector2 targetPosition)
    {
        float angle = GetAngleFromVectors(currentPosition, targetPosition);
        return Quaternion.Euler(0f, 0f, angle - 90);
    }

    public static float GetAngleFromVectors(Vector2 vectorA, Vector2 vectorB)
    {
        Vector2 difference = vectorA.GetDirectionTo(vectorB);
        // Vector2 difference = vectorB - vectorA;
        // difference.Normalize();


        return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    }
}
