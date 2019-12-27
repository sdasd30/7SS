﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    StatTracker plrScore;
    public int difficultyLevel = 1;
    private int requiredScore;
    private int sumOfScore;

    private int tmp;

    private void Start()
    {
        plrScore = FindObjectOfType<StatTracker>();
        requiredScore = 49;
        difficultyLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(getDifficulty());
        tmp = requiredScore + sumOfScore;
        Debug.Log((plrScore.currentScore));
        Debug.Log("rs:" + requiredScore);
        Debug.Log("tm:" + tmp);
        //requiredScore = 49 * difficultyLevel;
        if (requiredScore <= plrScore.currentScore-sumOfScore)
        {
            sumOfScore += requiredScore;
            requiredScore = (int) ((double)requiredScore * 1.3);
            difficultyLevel++;
        }
            
    }


    public int getDifficulty()
    {
        return difficultyLevel;
    }
}
