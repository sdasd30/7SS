using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour {
	public List<GameObject> weapons;
	public float weaponSwitchDelay;
	public GameObject nextWeapon;
	public GameObject currWeapon;
	public float cooldown;

	void Start(){
		cooldown = weaponSwitchDelay;
		nextWeapon = weapons [Random.Range (0, weapons.Count)];
		currWeapon = GameObject.Instantiate (nextWeapon, transform.position + new Vector3(.75f,0,-1f), Quaternion.identity);
		currWeapon.transform.parent = transform;
		nextWeapon = null;
	}

	void Update(){
		cooldown -= 1 * Time.deltaTime;

		if (nextWeapon == null || nextWeapon.GetComponent<CurrentWeapon>().name == transform.GetChild(0).GetComponent<CurrentWeapon>().name) {
			nextWeapon = weapons [Random.Range (0, weapons.Count)];
		}
		if (cooldown <= 0){
			GameObject temp = currWeapon;
			GameObject.Destroy(currWeapon);
			currWeapon = GameObject.Instantiate (nextWeapon, temp.transform.position, Quaternion.identity);
			currWeapon.transform.parent = transform;
			currWeapon.transform.localRotation = Quaternion.Euler (new Vector3(0f,0f,0f));
			nextWeapon = null;
			cooldown = weaponSwitchDelay;
		}

	}
}
