using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScrambleAll : PowerUpBase
{
    // Start is called before the first frame update
    public override void TriggerEffect(GameObject target)
    {
        if (target.GetComponentInChildren<WeaponHandler>() != null)
        {
            target.GetComponentInChildren<WeaponHandler>().RandomizeWeapons();
        }

    }
}
