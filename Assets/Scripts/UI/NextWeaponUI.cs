using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWeaponUI : MonoBehaviour {
	public GameObject WeaponHandler;
	GameObject theWeapon;
	public Sprite weaponSprite;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		theWeapon = WeaponHandler.GetComponent<WeaponHandler> ().nextWeapon;
		weaponSprite = theWeapon.GetComponent<SpriteRenderer> ().sprite;
		GetComponent<Image> ().sprite = weaponSprite;
		Rect sprite = weaponSprite.rect;
		GetComponent<RectTransform> ().localScale = new Vector3(1,sprite.height/sprite.width,0);
	}
}
