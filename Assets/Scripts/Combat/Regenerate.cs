using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    // Start is called before the first frame update
    private Attackable myAttackable;
    public float regenCooldown = .05f; //In seconds. Default is every 50 miliseconds
    public float regenRate;
    private float regenCooldownMax;
    void Start()
    {
        myAttackable = GetComponent<Attackable>();
        regenCooldownMax = regenCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (myAttackable != null) Regen();
    }
    private void Regen()
    {
        regenCooldown -= Time.deltaTime;
        if (regenCooldown <= 0)
        {
            if (myAttackable.hp <= myAttackable.maxHP)
            {
                myAttackable.hp += regenRate;
                if (myAttackable.hp > myAttackable.maxHP)
                {
                    myAttackable.hp = myAttackable.maxHP;
                }
            }
            regenCooldown = regenCooldownMax;
        }
    }
}
