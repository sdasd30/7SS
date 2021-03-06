﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WeaponStats : MonoBehaviour {

	public GameObject bullet;
	public string name; //Has no effect on gameplay, is just for ID purposes.
	public float spread; //In degrees.
	public float firerate; //In miliseconds.
	public float damage = 1;
	public float speed = 10; //Projectile Speed.
	public float duration = 5; //How long the bullet lasts in seconds.
	public int shots = 1; //How many shots does the gun shoot at once?
    public int pierce = 0; //How many enemies can this gun pierce;
    public float knockbackMult = 1; //Multiplier of knockback. 1 is mean does base knockback.
    public Vector3 Offset; //How much offset does it have?
    public List<DeathDropItem> OnDeathCreate;
    public float GravityScale = 0f;
    public float timeToGravity = 0f; //How many seconds of flat travel before it begins to fall to gravity?
    public float RecoilDamage = 0f; //How much damage does it do to the player per shot
    public Sprite Icon;
    public int Cost;
	//public int burstShots = 1;
	//public float burstDelay = 0;
	public bool auto = false; //Is full auto?
    public bool deflector = false;
    public List<AudioClip> attackSound; //What sounds should play when a projectile is created?

}
