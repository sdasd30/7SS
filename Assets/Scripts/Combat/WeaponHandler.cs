using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {
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
		nextWeaponLoad();
        //fillLoadout();
    }

	void Update(){
		cooldown -= Time.deltaTime;
        if (nextWeapon.GetComponent<WeaponStats>().name == transform.GetChild(0).GetComponent<WeaponStats>().name) nextWeaponLoad();
        if (cooldown <= 0){
            //Debug.Log("Cooldown Low");
            fillLoadout();
		}

	}

    private void fillLoadout()
    {
        cooldown = weaponSwitchDelay;
        GameObject temp = currWeapon;
        GameObject.Destroy(currWeapon.gameObject);
        currWeapon = GameObject.Instantiate(nextWeapon, temp.transform.position, Quaternion.identity);
        currWeapon.transform.parent = transform;
        currWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        nextWeaponLoad();

    }

    private void nextWeaponLoad()
    {
        nextWeapon = weapons[Random.Range(0, weapons.Count)];
        while (nextWeapon.GetComponent<WeaponStats>().name == transform.GetChild(0).GetComponent<WeaponStats>().name)
        {
            nextWeapon = weapons[Random.Range(0, weapons.Count)];
        }
        return;
    }
}
