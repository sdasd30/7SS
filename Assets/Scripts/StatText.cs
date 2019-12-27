using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{

    StatTracker stats;
    Text mText;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<StatTracker>();
        mText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        mText.text = "Your Stats:" +
            "\nLifetime Enemies Killed:" + stats.LifetimeEnemykills +
            "\nLifetime Weapon Kills" + stats.LifetimeWeaponKills +
            "\nLifetime Weapon Scores" + stats.LifetimeWeaponScores +
            "\nLifetime Weapon Switches" + stats.LifetimeWeaponSwitches +
            "\n Max Score" + stats.maxScore;
    }
}
