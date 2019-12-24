using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour {
	public GameObject WeaponHandler;
	WeaponHandler theWeapon;
	Slider me;
	// Use this for initialization
    public void init(GameObject x)
    {
        me = this.GetComponent<Slider>();
        WeaponHandler = x.GetComponentInChildren<WeaponHandler>().gameObject;
        theWeapon = WeaponHandler.GetComponent<WeaponHandler>();
    }

	// Update is called once per frame
	void Update () {
        if (WeaponHandler != null)
        {
            me.minValue = -theWeapon.weaponSwitchDelay;
            me.value = -theWeapon.cooldown;
        }
	}
}
