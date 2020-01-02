using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMirror : MonoBehaviour {
	SpriteRenderer thisObject;
    public bool hasParent;
	void Start(){
        thisObject = GetComponent<SpriteRenderer> ();
        if (transform.parent != null) hasParent = true;
	}
	// Update is called once per frame
	void Update () {
        if (hasParent)
        {
            if (transform.parent.rotation.z < -.707 || transform.parent.rotation.z > .707)
            {
                thisObject.flipY = true;
            }
            else
                thisObject.flipY = false;
        }
        else
        {
            if (transform.rotation.z < -.707 || transform.rotation.z > .707)
            {
                thisObject.flipY = true;
            }
            else
                thisObject.flipY = false;
        }
	}
}
