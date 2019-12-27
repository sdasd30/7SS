﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
    internal void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<FactionHolder>() != null)
        {
            if (GetComponent<FactionHolder>() != null)
            {
                if (GetComponent<FactionHolder>().Faction == FactionType.NEUTRAL || 
                    GetComponent<FactionHolder>().Faction == other.GetComponent<FactionHolder>().Faction)
                {
                    TriggerEffect(other.gameObject);
                    Destroy(gameObject);
                }
            } else
            {
                TriggerEffect(other.gameObject);
                Destroy(gameObject);
            }
        }
        
    }
    public virtual void TriggerEffect(GameObject target) { }
}
