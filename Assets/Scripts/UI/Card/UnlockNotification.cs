using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockNotification : MonoBehaviour
{
    public Text WeaponName;
    public Text ChallengeDescription;
    public Text Title;
    public Image Icon;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInfo(Sprite icon,string itemName, string achivement, string description)
    {
        Title.text = "Challenge Complete:\n" + achivement;
        ChallengeDescription.text = description;
        WeaponName.text = "New Weapon:\n" + itemName;
        Icon.sprite = icon;
    }

    public void CloseNotification()
    {
        Destroy(gameObject);
    }
}
