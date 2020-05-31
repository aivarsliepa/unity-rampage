using Boo.Lang;
using UnityEngine;

public class PlayerController : ObjectWithHealth
{
    Rigidbody2D rigidbody2d;
    Vector2 movement = new Vector2(0, 0);

    public float moveForce = 5000f;
    public float maxVelocity = 5f;
    public float slowDown = 0.9f;

    Crosshair crosshair;
    public Healthbar healthbar;

    private Gun[] guns;
    private Gun activeGun;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        crosshair = FindObjectOfType<Crosshair>();
        guns = GetComponentsInChildren<Gun>(true);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);

        if (Input.GetButton("Fire1"))
        {
            activeGun?.FireAt(crosshair.transform.position);
        }
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

        transform.rotation = Utils.GetRotationAngle(transform.position, crosshair.transform.position);
    }

    public bool PickWeapon(GunType gunType)
    {
        foreach (Gun gun in guns)
        {
            if (gun.Stats.gunType == gunType)
            {
                activeGun = gun;
                gun.gameObject.SetActive(true);
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }

        return true;
    }

    protected override void Die()
    {
        Instantiate(deathObject, transform.position, Quaternion.identity);
    }

    public override void ChangeHealth(int deltaHealth)
    {
        base.ChangeHealth(deltaHealth);
        healthbar.SetHealth(currentHealth);
    }
}
