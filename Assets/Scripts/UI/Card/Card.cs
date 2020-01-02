using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 1

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardUIManager UIManager;
    public bool InHand = false;
    public Sprite LockSprite;

    public Text MyCardName;
    public Text MyCardCost;
    public Image MyCardImage;
    public GameObject Weapon;

    private string m_description;

    private bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClicked()
    {
        if (!locked)
            UIManager.SwapCard(this);
    }

    public void SetCardInfo(Sprite s, string CardName, int weaponCost,GameObject wp)
    {
        MyCardImage.sprite = s;
        MyCardName.text = CardName;
        MyCardCost.text = "Cost: " + weaponCost;
        m_description = "";
        Weapon = wp;
    }
    public void SetLockCardInfo(string AchievementName, string AchievementDescription)
    {
        MyCardImage.sprite = LockSprite;
        MyCardName.text = "LOCKED" ;
        MyCardCost.text = AchievementName;
        m_description = AchievementDescription;
        locked = true;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UIManager != null && m_description != "")
            UIManager.ExtraMessage.text = MyCardName.text + ": " + m_description;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (UIManager != null && m_description != "")
            UIManager.ExtraMessage.text = "";
    }


}
