using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScramble : PowerUpBase
{
    public override void TriggerEffect(GameObject target)
    {
        if (target.GetComponentInChildren<WeaponHandler>() != null)
        {
            GameObject.Destroy(target.GetComponentInChildren<WeaponHandler>().currWeapon.gameObject);
            target.GetComponentInChildren<WeaponHandler>().begin();
        }

    }
}
