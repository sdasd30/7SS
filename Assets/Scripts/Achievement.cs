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
            Description = autoGenDescription();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string autoGenDescription()
    {
        string s = "";
        if (typeOfAchievement == AchievementType.WEAPON_KILLS)
        {
            s += "Kill " + value + " enemies ";
            if (WithObject != null)
                s = s + " with the " + WithObject.GetComponent<WeaponStats>().name + " ";
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
                    return st.MaxWeaponScores[withName] >= value;
                }
            } else
            {
                if (WithObject == null)
                {
                    return st.SumStat(st.LifetimeWeaponScores) >= value;
                }
                else
                {
                    return st.LifetimeWeaponScores[withName] >= value;
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
                    return st.MaxWeaponKills[withName] >= value;
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
                    return st.LifetimeWeaponScores[withName] >= value;
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
                return st.MaxWeaponUsedAtLevel[withName] >= value;
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
                    return st.MaxWeaponSwitches[withName] >= value;
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
                    return st.LifetimeWeaponSwitches[withName] >= value;
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
                    return st.MaxEnemykills[withName] >= value;
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
                    return st.LifetimeEnemykills[withName] >= value;
                }
            }
        }
        return false;
    }
}
