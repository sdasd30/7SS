using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerate : MonoBehaviour
{
    // Start is called before the first frame update
    private Attackable myAttackable;
    //public float regenCooldown = .05f; //In seconds. Default is every 50 miliseconds
    public float regenRate; //Howmuch heals after one second
    //private float regenCooldownMax;
    void Start()
    {
        myAttackable = GetComponent<Attackable>();
        //regenCooldownMax = regenCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (myAttackable != null) Regen();
    }


    private void Regen()
    {
        myAttackable.hp += regenRate * Time.deltaTime;
    }

    public float getRegen()
    {
        return regenRate;
    }

    public void TemporaryRegenChange(float time, float newRate)
    {
        StartCoroutine(crTemporaryRegenChange(time,newRate));
    }

    IEnumerator crTemporaryRegenChange(float time, float newRate)
    {
        float oldRate = regenRate;
        regenRate = newRate;
        Debug.Log("New rate is " + newRate);
        yield return new WaitForSeconds(time);
        regenRate = oldRate;
        Debug.Log("New rate is " + oldRate);
    }
}
