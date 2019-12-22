using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    public GameObject player;
    Text me;
    int maxHP;
    int currentHP;
    // Start is called before the first frame update
    void Start()
    {
        me = this.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = (int) player.GetComponent<Attackable>().hp;
        maxHP = (int) player.GetComponent<Attackable>().maxHP;
        me.text = "HP: " + currentHP + "/" + maxHP;
    }
}
