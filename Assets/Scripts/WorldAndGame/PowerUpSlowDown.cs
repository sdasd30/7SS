using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSlowDown : PowerUpBase
{
    public float LongCooldown = 14f;

    public override void TriggerEffect(GameObject target)
    {
        if (target.GetComponentInChildren<WeaponHandler>() != null)
        {
            target.GetComponentInChildren<WeaponHandler>().SlowDown(LongCooldown);
        }

    }
}
