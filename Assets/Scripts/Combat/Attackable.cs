﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Attackable : MonoBehaviour {
	public float maxHP = 1;
	public float hp = 1;
	public bool allied;
	public bool anarchy;
	public bool alive = true;
	public bool important;
	public float deathbarrier = -9999;
	public float regenRate;
	public float regenCooldown;
	// Use this for initialization
	void Start () {
		hp = maxHP;
		regenCooldown = 1;
	}

	// Update is called once per frame
	void Update () {
        checkDead();
		FallDown ();
		Regen ();
	}

    public void checkDead()
    {
        if (hp <= 0)
        {
            alive = false;
        }
        if (!alive)
        {
            Destroy(this.gameObject);
        }
    }

	public void TakeDamage(float damage){
		hp -= damage;
	}
    public void TakeKnockback(Vector2 vec)
    {
        GetComponent<PhysicsSS>().addToVelocity(vec);
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
				hp += regenRate;
				if (hp > maxHP) {
					hp = maxHP;
				}
			}
			regenCooldown = .05f;
		}
	}
	public bool CanAttack(Attackable otherObj)
    {
        return anarchy || (allied && !otherObj.allied) ||
            (!allied && otherObj.allied);
    }
}