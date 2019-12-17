using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour {
	// Update is called once per frame
	public Vector2 offset;
	void Update () {
		Vector3 mousePos = Input.mousePosition;

		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		//transform.position = transform.parent.position + new Vector3 (offset.x * Mathf.Cos (angle), offset.y * Mathf.Sin (angle), 0);
	}
}