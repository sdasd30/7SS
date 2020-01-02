using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType {SCORE,WEAPON_KILLS,LEVEL,ENEMY_KILLED,WEAPON_SWITCHES, POWER_UP}
public class Achievement : MonoBehaviour
{
    public string DisplayName;
    public string Description;
    public AchievementType typeOfAchievement;
    public float value;
    public GameObject WithObject;
    public GameObject WithObject2;
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
    public string GetAchievementDescription()
    {
        if (Description.Length == 0)
            Description = AutoGenDescription();
        return Description;
    }
    public string AutoGenDescription()
    {
        string s = "";
        if (typeOfAchievement == AchievementType.WEAPON_KILLS)
        {
            string sf = (value > 1) ? "enemies " : "enemy ";
            s += "Destroy " + value + " " + sf;
            if (WithObject != null)
                s = s + "with the " + getWeaponName(WithObject) + " ";
            if (InOneLife)
                s += "in one life";
            else
                s += "in total";
        } else if (typeOfAchievement == AchievementType.SCORE)
        {
            s += "Score " + value + " points ";
            if (WithObject != null)
                s = s + "with the " + getWeaponName(WithObject) + " ";
            if (InOneLife)
                s += "in one life";
            else
                s += "in total";
        }
        else if (typeOfAchievement == AchievementType.ENEMY_KILLED)
        {
            s += "Destroy " + value + " ";
            if (WithObject != null)
            {
                if (value > 1)
                    s += WithObject.GetComponent<Score>().TrackName + "s ";
                else
                    s += WithObject.GetComponent<Score>().TrackName + " ";
            }
            else
                s += "enemies ";
            if (WithObject2 != null)
                s += "with the " + getWeaponName(WithObject2) + " ";
            if (InOneLife)
                s += "in one life";
            else
                s += "in total";
        }
        else if (typeOfAchievement == AchievementType.WEAPON_SWITCHES)
        {
            if (WithObject != null)
                s = "Use the " + getWeaponName(WithObject) + " " + value + " times ";
            else
                s = "Switch weapons " + value + " times ";
            if (InOneLife)
                s += "in one life";
            else
                s += "in total";
        }
        else if (typeOfAchievement == AchievementType.POWER_UP)
        {
            if (WithObject != null)
                s = "Pick up the " + WithObject.GetComponent<PowerUpBase>().PowerUpID + " item " + value + " times in one life";
        }
        else if (typeOfAchievement == AchievementType.LEVEL)
        {
            s += "Reach Level " + value + " ";
            if (WithObject != null)
                s += "with the " + getWeaponName(WithObject);       
        }
        return s;
    }

    private string getWeaponName(GameObject go)
    {
        if (go.GetComponent<WeaponStats>() == null)
            return "";
        if (go.GetComponent<Achievement>() == null || go.GetComponent<Achievement>().CheckAchievementMet())
            return go.GetComponent<WeaponStats>().name;
        return "??????";
    }
    public bool CheckAchievementMet()
    {
        StatTracker st = FindObjectOfType<StatTracker>();
        string withName = "";
        if (WithObject != null) {
            if (WithObject.GetComponent<Score>() != null)
                withName = WithObject.GetComponent<Score>().TrackName;
            else if (WithObject.GetComponent<WeaponStats>() != null)
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
                    if (WithObject2 != null)
                        return st.IsAchievementMet(st.MaxEnemyWeaponKills, withName,WithObject2.GetComponent<WeaponStats>().name, (int)value);
                    else
                        return st.IsAchievementMet(st.MaxEnemyKills,withName, (int) value);
                }
            }
            else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeEnemyKills) >= value;
                }
                else
                {
                    if (WithObject2 != null)
                        return st.IsAchievementMet(st.LifetimeEnemyKills, withName, WithObject2.GetComponent<WeaponStats>().name, (int)value);
                    else
                        return st.IsAchievementMet(st.LifetimeEnemyKills,withName, (int)value);
                }
            }
        }
        else if (typeOfAchievement == AchievementType.POWER_UP)
        {
            if (WithObject == null)
            {
                return false;
            }
            else
            {
                 return st.IsAchievementMet(st.MaxPowerUps, WithObject.GetComponent<PowerUpBase>().PowerUpID, (int)value);
            }
        }
        return false;
    }
}
