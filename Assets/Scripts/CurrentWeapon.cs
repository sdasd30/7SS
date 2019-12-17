using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour {

	public GameObject bullet;
	public string name; //Has no effect on gameplay, is just for ID purposes.
	public float spread; //In degrees.
	public float firerate; //In miliseconds.
	public float damage = 1;
	public float speed = 10; //Projectile Speed.
	public float duration = 5; //How long the bullet lasts in seconds.
	public int shots = 1; //How many shots does the gun shoot at once?
	//public int burstShots = 1;
	//public float burstDelay = 0;
	public bool auto = false; //Is full auto?

}
