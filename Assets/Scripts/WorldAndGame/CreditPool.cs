using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPool : MonoBehaviour
{
    public float creditMod = 1;  //Default is 1. That means the game will get 1 credit to spend a second.
                                //Halfing the rate at which things spawn is done by making credit count .5, and so on.
    public float creditPool = 0;
    public float incrementby = 1; //Public only for viewing in inspector.
    private float creditCooldown;
    private Difficulty gameDifficulty;
    public int knownDifficulty;
    // Start is called before the first frame update
    void Start()
    {
        gameDifficulty = GetComponent<Difficulty>();
        knownDifficulty = gameDifficulty.getDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkDifficulty(knownDifficulty))
        {
            knownDifficulty++;
            newIncrement(incrementby);
        }
        moreCredits(incrementby);

    }

    public bool spendCredit(float requested)
    {
        Debug.Log(requested + " requested, " + creditPool + "owned");
        if (requested < creditPool)
        {
            creditPool -= requested;
            //Debug.Log("Spent, sending true");
            return true;
        }
        //Debug.Log("Rejected, sending false");
        return false;
    }

    private bool checkDifficulty(int kd)
    {
        if(kd < gameDifficulty.getDifficulty()) return true;
        return false;
    }

    private void newIncrement(float inc)
    {
        incrementby = inc + (inc * .21f);
    }

    private void moreCredits(float inc)
    {
        creditCooldown -= Time.deltaTime;
        if (creditCooldown <= 0)
        {
            creditPool += inc;
            creditCooldown = 1;
        }
    }
}
