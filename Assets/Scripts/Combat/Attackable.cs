﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FactionHolder))]
public class Attackable : MonoBehaviour {
	public float maxHP = 1;
	public float hp = 1;
	private bool alive = true;


    private WeaponStats m_lastWeaponHurtBy;
    // Use this for initialization
    void Start () {
		hp = maxHP;
        if (GetComponent<FactionHolder>() == null)
            gameObject.AddComponent<FactionHolder>();
	}

	// Update is called once per frame
	void Update () {
        if (hp > maxHP) hp = maxHP;
        checkDead();
	}

    public void checkDead()
    {
        if (hp <= 0 && alive)
        {
            alive = false;
            if (m_lastWeaponHurtBy != null && GetComponent<Score>() != null)
            {
                FindObjectOfType<StatTracker>().TrackKill(GetComponent<Score>(), m_lastWeaponHurtBy);
            }
        }
        if (!alive)
        {
            Destroy(this.gameObject);
        }
    }

	public void TakeDamage(float damage){
		hp -= damage;
        hp = Mathf.Max(0f, Mathf.Min(maxHP, hp));
	}

    public void TakeKnockback(Vector2 vec)
    {
        GetComponent<PhysicsSS>().addToVelocity(vec);
    }

    public void TakeHit(HitInfo hi)
    {
        TakeDamage(hi.Damage);
        TakeKnockback(hi.Knockback);
        m_lastWeaponHurtBy = hi.OriginWeapon;
    }
}
