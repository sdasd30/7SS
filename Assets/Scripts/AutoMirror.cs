using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMirror : MonoBehaviour {
	SpriteRenderer thisObject;
	public bool flipped;
	void Start(){
        thisObject = GetComponent<SpriteRenderer> ();
	}
	// Update is called once per frame
	void Update () {
		if (transform.parent.rotation.z < -.707 || transform.parent.rotation.z> .707){
			thisObject.flipY = true;
			flipped = true;
		}
		else
            thisObject.flipY = false;
			flipped = false;
	}
}
