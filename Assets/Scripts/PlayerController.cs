using Boo.Lang;
using System.Linq;
using UnityEngine;

public class PlayerController : ObjectWithHealth
{
    // movement
    Rigidbody2D rigidbody2d;
    Vector2 movement = new Vector2(0, 0);
    public float moveForce = 5000f;
    public float maxVelocity = 5f;
    public float slowDown = 0.9f;

    // UI
    Crosshair crosshair;
    Healthbar healthbar;
    WeaponBar weaponBar;

    // Guns
    private WeaponBase[] weapons;
    private WeaponBase activeWeapon;
    private List<GunType> weaponList;
    private int activeGunIndex = -1;

    void Awake()
    {
        weaponList = new List<GunType>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        crosshair = FindObjectOfType<Crosshair>();
        healthbar = FindObjectOfType<Healthbar>();
        weaponBar = FindObjectOfType<WeaponBar>();
        weapons = GetComponentsInChildren<WeaponBase>(true);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(horizontal, vertical);

        if (Input.GetButton("Fire1"))
        {
            activeWeapon?.FireAt(crosshair.transform.position);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            activeWeapon?.StopFiring();
        }

        var scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel > 0f)
        {
            SelectWeapon(++activeGunIndex);
        }
        else if (scrollWheel < 0f)
        {
            SelectWeapon(--activeGunIndex);
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
        if (weaponList.Contains(gunType))
            return false;

        weaponList.Add(gunType);
        SelectWeapon(weaponList.Count - 1);

        return true;
    }

    private void SelectWeapon(int index)
    {
        if (index < 0)
        {
            index = weaponList.Count - 1;
        } else if (index >= weaponList.Count)
        {
            index = 0;
        }

        activeGunIndex = index;
        var gunType = weaponList.ElementAt(index);
        weaponBar.SetActiveWeapon(gunType);

        foreach (WeaponBase weapon in weapons)
        {
            if (weapon.gunType == gunType)
            {
                activeWeapon = weapon;
                weapon.gameObject.SetActive(true);
                weapon.StopFiring();
            }
            else
            {
                weapon.StopFiring();
                weapon.gameObject.SetActive(false);
            }
        }
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
