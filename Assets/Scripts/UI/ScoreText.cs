using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    Difficulty dif;
    StatTracker scr;
    Text mText;
    void Start()
    {
        dif = FindObjectOfType<Difficulty>();
        scr = FindObjectOfType<StatTracker>();
        mText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
        mText.text = "Score: " + scr.currentScore + "\nDifficulty: " + dif.getDifficulty();
    }
}
