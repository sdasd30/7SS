using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHazard : WeaponStats
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Hitbox>() != null)
        {
            Hitbox hb = GetComponent<Hitbox>();
            hb.OriginWeapon = this;
            Debug.Log("Setting origin Weapon");
            //hb.Damage = damage;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
