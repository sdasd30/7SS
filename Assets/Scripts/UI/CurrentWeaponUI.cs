using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWeaponUI : MonoBehaviour {
	public GameObject player;
    WeaponHandler playerHandler;
	GameObject theWeapon;
	public Sprite weaponSprite;
	// Use this for initialization
	void Start () {
        playerHandler = player.GetComponentInChildren<WeaponHandler>();
    }

	// Update is called once per frame
	void Update () {
        if (player != null) updateSprite();
    }

    private void updateSprite()
    {
        theWeapon = playerHandler.currWeapon;
        weaponSprite = theWeapon.GetComponent<SpriteRenderer>().sprite;
        GetComponent<Image>().sprite = weaponSprite;
        Rect sprite = weaponSprite.rect;
        GetComponent<RectTransform>().localScale = new Vector3(1, sprite.height / sprite.width, 0);
    }

}
