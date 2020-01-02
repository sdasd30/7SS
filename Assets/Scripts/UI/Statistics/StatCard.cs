using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatCard : MonoBehaviour
{
    public StatScreenManager UIManager;

    public Text MyCardName;
    public Text MyCardCost;
    public Image MyCardImage;
    public GameObject Weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStatCardClick()
    {
        UIManager.ShowCardStats(this);
    }

    public void SetCardInfo(Sprite s, string CardName, int weaponCost, GameObject wp)
    {
        MyCardImage.sprite = s;
        MyCardName.text = CardName;
        MyCardCost.text = "Cost: " + weaponCost;
        Weapon = wp;
    }
}
