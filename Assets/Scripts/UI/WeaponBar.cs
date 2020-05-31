using UnityEngine;

public class WeaponBar : MonoBehaviour
{
    private WeaponType[] weapons;

    void Awake()
    {
        weapons = GetComponentsInChildren<WeaponType>(true);
    }

   public void SetActiveWeapon(GunType gunType)
    {
        foreach(WeaponType gun in weapons)
        {
            if (gun.gunType == gunType)
            {
                gun.gameObject.SetActive(true);
            } else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }
}
