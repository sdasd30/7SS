using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float score = 0;

    public float retrieveScore()
    {
        return score;
    }

    public void addScore(float add)
    {
        score += add;
    }
}
