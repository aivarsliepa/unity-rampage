using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public bool useController = true;
    public float cursorSpeed = 30f;
    public float distance = 3f;

    PlayerController player;

    public float previousAngle = 0f;

    public bool freeAim = false;

    Camera mainCamera;

    public int freeAimBoundsOffset = 1;

    void Start()
    {
        Cursor.visible = false;
        player = FindObjectOfType<PlayerController>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {

        if (useController)
        {
            if (Input.GetAxisRaw("AimDown") != 0)
            {
                ControllerFreeAim();
                return;
            }
            freeAim = false;

            Vector2 aimNormalized = GetAimVector().normalized;

            if (Mathf.Approximately(aimNormalized.magnitude, 0f))
            {
                ControllerFallowPlayer();
            }
            else
            {
                ControllerNormalAim(aimNormalized);
            }
        }
        else
        {
            MouseAim();
        }

    }

    Vector2 GetAimVector()
    {
        float aimHorizontal = Input.GetAxis("AimHorizontal");
        float aimVertical = Input.GetAxis("AimVertical");
        return new Vector2(aimHorizontal, aimVertical);
    }

    void ControllerFreeAim()
    {
        freeAim = true;

        var aim = GetAimVector() * cursorSpeed * Time.deltaTime;
        var newPos = (Vector2)transform.position + aim;

        var playerPos = player.transform.position;

        var verticalDelta = 2f * mainCamera.orthographicSize - freeAimBoundsOffset;
        var horizontalDelta = mainCamera.aspect * 2f * mainCamera.orthographicSize - freeAimBoundsOffset;

        newPos = new Vector2(
            Mathf.Clamp(newPos.x, playerPos.x - horizontalDelta, playerPos.x + horizontalDelta),
            Mathf.Clamp(newPos.y, playerPos.y - verticalDelta, playerPos.y + verticalDelta)
        );

        transform.position = newPos;
    }

    void MouseAim()
    {
        // Use Vector2  to ignore Z-Axis (it can hide crosshair)
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
    }

    void ControllerNormalAim(Vector2 aimDirection)
    {
        Vector2 playerPos = player.transform.position;
        Vector2 newPosition = playerPos + aimDirection * distance;
        transform.position = newPosition;

        // store angle
        previousAngle = Utils.GetAngleFromVectors(playerPos, newPosition);
    }

    void ControllerFallowPlayer()
    {
        Vector2 rotated = new Vector2(distance, 0).Rotate(previousAngle);
        Vector2 playerPos = player.transform.position;
        Vector2 pos = playerPos + rotated;
        transform.position = pos;
    }
}
