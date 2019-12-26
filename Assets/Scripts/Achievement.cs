using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType {SCORE,KILLS,LEVEL,WEAPON_SWITCHES}
public class Achievement : MonoBehaviour
{
    public string InternalID;
    public string DisplayName;
    public string Description;
    public AchievementType typeOfAchievement;
    public float value;
    public GameManager WithWeapon;
    public GameObject EnemyTargeted;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckAchievementMet()
    {
        if (typeOfAchievement == AchievementType.SCORE)
        {

        }
        else if (typeOfAchievement == AchievementType.KILLS)
        {

        }
        else if (typeOfAchievement == AchievementType.LEVEL)
        {
        } else if (typeOfAchievement == AchievementType.WEAPON_SWITCHES)
        {
        }
        
        return false;
    }
}
