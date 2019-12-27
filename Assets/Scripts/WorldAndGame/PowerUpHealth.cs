using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHealth : PowerUpBase
{
    public float Health = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void TriggerEffect(GameObject target) {
        if (target.GetComponent<Attackable>() != null)
            target.GetComponent<Attackable>().TakeDamage(-Health);
    }
}
