using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Attackable : MonoBehaviour {
	public float maxHP = 1;
	public float hp;
	public bool allied;
	public bool anarchy;
	public bool alive;
	public bool important;
	public float deathbarrier = -999999999999999;
	public float regenRate;
	float regenCooldown;
	// Use this for initialization
	void Start () {
		hp = maxHP;
		regenCooldown = 1;
	}

	// Update is called once per frame
	void Update () {
		if (hp <= 0){
			alive = false;
		}
		if (!alive) {
			Destroy (this.gameObject);
		}
		FallDown ();
		Regen ();
	}
	public void TakeDamage(float damage){
		hp -= damage;
	}
	public float ReturnHP() {
		return hp;
	}
	public void FallDown(){
		if (alive && transform.position.y < deathbarrier) {
			if (important) {
				hp -= Mathf.RoundToInt ((hp * .25f));
				this.transform.position = new Vector3 (0, 0, 0);
			} else {
				Destroy (this.gameObject);
			}
		}
	}

	void Regen(){
		regenCooldown -= Time.deltaTime;
		if (regenCooldown <= 0) {
			if (hp <= maxHP) {
				hp += maxHP * regenRate;
				if (hp > maxHP) {
					hp = maxHP;
				}
			}
			regenCooldown = 1;
		}
	}
		
}
