using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (FactionHolder))]
public class Attackable : MonoBehaviour {
	public float maxHP = 1;
	public float hp = 1;
	private bool alive = true;

    // Use this for initialization
    void Start () {
		hp = maxHP;
        if (GetComponent<FactionHolder>() == null)
            gameObject.AddComponent<FactionHolder>();
	}

	// Update is called once per frame
	void Update () {
        checkDead();
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
}
