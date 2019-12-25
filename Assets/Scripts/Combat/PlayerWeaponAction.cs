using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponAction : MonoBehaviour {
	//public Rigidbody projectile;
	//public GameObject BulletPrefab;
	WeaponStats Weapon;
	public Vector2 Offset;
	//bool firing; //Why does this variable exist?
	float coolDown = 0;
	// Use this for initialization
	void Start () {
		Weapon = GetComponent<WeaponStats> ();
        transform.parent.parent.GetComponent<BulletShooter>().Weapon = Weapon;
    }

	// Update is called once per frame
	void Update () {
		//if (!firing) {
		//	if (Weapon.auto) {
		//		if (Input.GetButton ("Fire1")) {
		//			if (coolDown <= 0) {
		//				//for (int c = 0; c < Weapon.burstShots; c++) {
		//					fire ();
		//					//StartCoroutine(WaitSeconds());
		//				//}
		//			}
		//		}
		//	} else {
		//		if (Input.GetButtonDown ("Fire1")) {
		//			if (coolDown <= 0) {
		//				//for (int c = 0; c < Weapon.burstShots; c++) {
		//					fire ();
		//				//StartCoroutine(WaitSeconds());
		//				//}
		//			}
		//		}
		//	}
				
		//	if (coolDown >= 0) {
		//		coolDown -= 1 * Time.deltaTime;
		//	}
		//}
	}

	public void fire(){
		for (int i = 0; i < Weapon.shots; i++) {
            Vector3 rawTargetPoint = transform.parent.GetComponent<FaceMouse>().GetMousePos();
            float rawAngle = Mathf.Atan2(rawTargetPoint.y, rawTargetPoint.x) * Mathf.Rad2Deg;
            //float angle = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            //GameObject bullet = GameObject.Instantiate (Weapon.bullet, transform.position + new Vector3 (Offset.x * Mathf.Cos (angle), Offset.y * Mathf.Sin (angle), +.5f), Quaternion.identity);
            //bullet.GetComponent<PlayerProjectile> ().SetAngle (transform.rotation.eulerAngles.z + Random.Range (-Weapon.spread, Weapon.spread) - 90);
            //bullet.GetComponent<PlayerProjectile> ().SetWeapon (Weapon);
            //         transform.parent.parent.GetComponent<FactionHolder>().SetFaction(bullet);
            //Destroy (bullet, Weapon.duration);

            //GameObject player = transform.parent.parent.gameObject;
            //player.GetComponent<BulletShooter>().fire(Weapon, targetPoint);
			}
		coolDown = Weapon.firerate / 1000;
		}




	IEnumerator WaitSeconds(){
		yield return new WaitForSeconds(0);
	}
}
