using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMeter : MonoBehaviour
{
    Slider mSlider;
    Difficulty difficulty;
    StatTracker score;
    // Start is called before the first frame update
    void Start()
    {
        mSlider = GetComponent<Slider>();
        difficulty = FindObjectOfType<Difficulty>();
        score = FindObjectOfType<StatTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        mSlider.maxValue = difficulty.getScoreNeeded();
        mSlider.value = score.currentScore - difficulty.getSumOfScore();
    }
}
