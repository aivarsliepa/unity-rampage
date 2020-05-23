using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCotroller : MonoBehaviour
{
    PlayerController player;
    Crosshair crosshair;
    private Vector3 cameraChangeVelocity;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        crosshair = FindObjectOfType<Crosshair>();
    }

    // Update is called once per frame
    void Update()
    {
        if (crosshair.freeAim)
        {
            SetNewPosition(GetCenterPoint(), 0.1f);
        }
        else
        {
            SetNewPosition(player.transform.position, 0.5f);
        }

    }

    void SetNewPosition(Vector2 newPos, float smoothTime) {
        var newPosition = new Vector3(newPos.x, newPos.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref cameraChangeVelocity, smoothTime);
    }

    Vector2 GetCenterPoint()
    {
        var bounds = new Bounds(player.transform.position, Vector3.zero);
        bounds.Encapsulate(crosshair.transform.position);
        return bounds.center;
    }
}
