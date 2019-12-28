using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpWeapon : PowerUpBase
{
    public float timeOn = 7;
    public override void TriggerEffect(GameObject target)
    {
        if (target.GetComponentInChildren<WeaponHandler>() != null)
        {
            target.GetComponentInChildren<WeaponHandler>().GiveSuperWeapon(timeOn);
        }

    }
}   
