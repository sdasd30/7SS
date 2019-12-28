using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeedUp : PowerUpBase
{
    public float lengthOfEffect = 14f;
    public float desiredCooldown = 4.5f;

    public override void TriggerEffect(GameObject target)
    {
        if (target.GetComponentInChildren<WeaponHandler>() != null)
        {
            target.GetComponentInChildren<WeaponHandler>().SpeedUp(lengthOfEffect,desiredCooldown);
        }

    }
}
