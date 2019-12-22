using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaining : MonoBehaviour {
	public GameObject WeaponHandler;
	WeaponHandler theWeapon;
	Slider me;
	// Use this for initialization
	void Start () {
		me = this.GetComponent<Slider> ();
		theWeapon = WeaponHandler.GetComponent<WeaponHandler> ();
	}

	// Update is called once per frame
	void Update () {
		me.minValue = -theWeapon.weaponSwitchDelay;
		me.value = -theWeapon.cooldown;
	}
}
