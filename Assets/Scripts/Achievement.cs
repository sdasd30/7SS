using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType {SCORE,WEAPON_KILLS,LEVEL,ENEMY_KILLED,WEAPON_SWITCHES}
public class Achievement : MonoBehaviour
{
    public string DisplayName;
    public string Description;
    public AchievementType typeOfAchievement;
    public float value;
    public GameObject WithObject;
    public bool InOneLife;
    // Start is called before the first frame update
    void Start()
    {
        if (Description.Length == 0)
            Description = AutoGenDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string AutoGenDescription()
    {
        string s = "";
        if (typeOfAchievement == AchievementType.WEAPON_KILLS)
        {
            s += "Destroy " + value + " enemies ";
            if (WithObject != null)
                s = s + "with the " + WithObject.GetComponent<WeaponStats>().name + " ";
            if (InOneLife)
                s += "in one life";
        } else if (typeOfAchievement == AchievementType.SCORE)
        {
            s += "Score " + value + " points ";
            if (WithObject != null)
                s = s + "with the " + WithObject.GetComponent<WeaponStats>().name + " ";
            if (InOneLife)
                s += "in one life";
        }
        else if (typeOfAchievement == AchievementType.ENEMY_KILLED)
        {
            s += "Destroy " + value;
            if (WithObject != null)
                s = WithObject.GetComponent<Score>().TrackName + "s ";
            else
                s = "enemies ";
            if (InOneLife)
                s += "in one life";
        }
        else if (typeOfAchievement == AchievementType.WEAPON_SWITCHES)
        {
            if (WithObject != null)
                s = "Use the " + WithObject.GetComponent<WeaponStats>().name + " " + value + " times ";
            else
                s = "Switch weapons " + value + " times ";
            if (InOneLife)
                s += "in one life";
        }
        return s;
    }
    public bool CheckAchievementMet()
    {
        StatTracker st = FindObjectOfType<StatTracker>();
        string withName = "";
        if (WithObject != null) {
            if (WithObject.GetComponent<Score>() != null)
                withName = WithObject.GetComponent<Score>().TrackName;
            else
                withName = WithObject.GetComponent<WeaponStats>().name;
        }
        if (typeOfAchievement == AchievementType.SCORE)
        {
            if (InOneLife)
            {
                if (WithObject == null)
                {
                    return st.maxScore > value;
                } else
                {
                    return st.IsAchievementMet(st.MaxWeaponScores,withName, (int)value);
                }
            } else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeWeaponScores) >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.LifetimeWeaponScores,withName, (int)value);
                }
            }
        }
        else if (typeOfAchievement == AchievementType.WEAPON_KILLS)
        {
            if (InOneLife)
            {
                if (WithObject == null)
                {
                    return st.maxKills >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.MaxWeaponKills,withName, (int)value);
                }
            }
            else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeWeaponKills) >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.LifetimeWeaponScores,withName,(int)value);
                }
            }
        }
        else if (typeOfAchievement == AchievementType.LEVEL)
        {
            if (WithObject == null)
            {
                return st.MaxVal(st.MaxWeaponUsedAtLevel) >= value;
            }
            else
            {
                return st.IsAchievementMet(st.MaxWeaponUsedAtLevel,withName,(int)value);
            }
        } else if (typeOfAchievement == AchievementType.WEAPON_SWITCHES)
        {
            if (InOneLife)
            {
                if (WithObject == null)
                {
                    return false;
                }
                else
                {
                    return st.IsAchievementMet(st.MaxWeaponSwitches, withName, (int)value);
                }
            }
            else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeWeaponSwitches) >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.LifetimeWeaponSwitches,withName,(int)value);
                }
            }
        }
        else if (typeOfAchievement == AchievementType.ENEMY_KILLED)
        {
            if (InOneLife)
            {
                if (WithObject == null)
                {
                    return st.maxKills >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.MaxEnemykills,withName, (int) value);
                }
            }
            else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeEnemykills) >= value;
                }
                else
                {
                    return st.IsAchievementMet(st.LifetimeEnemykills,withName, (int)value);
                }
            }
        }
        return false;
    }
}
