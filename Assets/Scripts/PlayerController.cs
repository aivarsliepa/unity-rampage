using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Vector2 movement = new Vector2(0, 0);

    public float moveForce = 5000f;
    public float maxVelocity = 5f;
    public float slowDown = 0.9f;

    public bool applySlowdown = true;

    Crosshair crosshair;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // crosshair = GetComponent<Crosshair>();
        // crosshair = GetComponentInChildren<Crosshair>();
        crosshair = FindObjectOfType<Crosshair>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rigidbody2d.AddForce(movement * moveForce);
            rigidbody2d.velocity = Vector2.ClampMagnitude(rigidbody2d.velocity, maxVelocity);
        }
        else if (rigidbody2d.velocity.magnitude < 0.1f)
        {
            rigidbody2d.velocity = Vector2.zero;
        }
        else if (!Mathf.Approximately(rigidbody2d.velocity.magnitude, 0f))
        {
            rigidbody2d.velocity = rigidbody2d.velocity * slowDown;
        }

        // transform.rotation = Utils.GetRotationAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        transform.rotation = Utils.GetRotationAngle(transform.position, crosshair.transform.position);
    }
}
