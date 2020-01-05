using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(WeaponHandler))]
public class ImTutorial : MonoBehaviour
{
    public List<GameObject> weapons;
    // Start is called before the first frame update
    void Start()
    {
        //weapons = GetComponent<WeaponHandler>().weapons;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<WeaponHandler>().ForceWeapons(weapons);
        FindObjectOfType<CardManager>().CurrentHand = weapons;
    }
}
