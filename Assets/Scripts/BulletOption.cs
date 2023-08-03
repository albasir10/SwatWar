using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOption : MonoBehaviour
{
    public WeaponOption weaponOption;
    public float timeToInactiveBullet = 5f;
    public float speedBullet = 10f;


    private void OnTriggerEnter(Collider other)
    {
        weaponOption.InactiveBall(this);

        Unit unit = other.GetComponentInParent<Unit>();

        if (unit != null)
        {
            unit.GetDamage(weaponOption.damageBullet);
        }

    }

}
