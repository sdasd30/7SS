using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    PlayerScore plrScore;
    public int difficultyLevel;

    private void Start()
    {
        plrScore = FindObjectOfType<PlayerScore>().GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        difficultyLevel = (int) (plrScore.getScore()/49) + 1;
    }

    public int getDifficulty()
    {
        return difficultyLevel;
    }
}
