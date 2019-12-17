using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMirror : MonoBehaviour {
	SpriteRenderer gun;
	public bool flipped;
	void Start(){
		gun = GetComponent<SpriteRenderer> ();
	}
	// Update is called once per frame
	void Update () {
		if (transform.parent.rotation.z < -.707 || transform.parent.rotation.z> .707){
			gun.flipY = true;
			flipped = true;
		}
		else
			gun.flipY = false;
			flipped = false;
	}
}
