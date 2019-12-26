using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float score = 0;

    public Dictionary<string, int> Enemykills;
    public Dictionary<string, int> WeaponKills;
    public Dictionary<string, int> ScoreKills;
    public float getScore()
    {
        return score;
    }

    public void addScore(float add)
    {
        score += add;
    }

    public void addDeath(GameObject deadObject) {
    }
}
