using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatTracker : MonoBehaviour
{
    private static StatTracker m_instance;
    public Dictionary<string, Dictionary<string, int>> CurrentEnemykills;
    public Dictionary<string, int> CurrentWeaponKills;
    public Dictionary<string, int> CurrentWeaponScores;
    public Dictionary<string, int> CurrentPowerUps;

    public Dictionary<string, Dictionary<string, int>> MaxEnemyWeaponKills;
    public Dictionary<string, int> MaxEnemyKills;
    public Dictionary<string, int> MaxWeaponKills;
    public Dictionary<string, int> MaxWeaponScores;

    public Dictionary<string, Dictionary<string, int>> LifetimeEnemyKills;
    public Dictionary<string, int> LifetimeWeaponKills;
    public Dictionary<string, int> LifetimeWeaponScores;

    public Dictionary<string, int> MaxWeaponSwitches;
    public Dictionary<string, int> CurrentWeaponSwitches;
    public Dictionary<string, int> LifetimeWeaponSwitches;

    public Dictionary<string, int> MaxWeaponUsedAtLevel;


    public Dictionary<string, int> MaxPowerUps;

    public int currentScore;
    public int maxScore;
    public int currentKills;
    public int maxKills;
    public int currentSwitches;
    public int maxSwitches;

    private string m_currentWeaponName = "";
    private GameObject m_currentPlayer;
    private bool Disabled = false;
    // Start is called before the first frame update
    void Awake()
    {

        if (m_instance == null)
        {
            m_instance = this;
            SceneManager.sceneLoaded += onSceneLoad;
            LoadStats();
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_currentPlayer != null)
        {
            CheckWeaponUpdate();
        }

    }
    public void SetPlayerObj(GameObject player)
    {
        m_currentPlayer = player;
    }
    private void CheckWeaponUpdate()
    {
        if (Disabled)
            return;
        WeaponStats realWeapon = m_currentPlayer.transform.GetChild(0).GetChild(0).gameObject.GetComponent<WeaponStats>();

        if (m_currentWeaponName == "")
        {
            m_currentWeaponName = realWeapon.name;
        } else if (m_currentWeaponName != realWeapon.name)
        {
            m_currentWeaponName = realWeapon.name;
            OnWeaponSwitch(realWeapon);
        }
    } 

    private void OnWeaponSwitch(WeaponStats weapon)
    {
        if (Disabled)
            return;
        currentSwitches++;
        maxSwitches = Mathf.Max(maxSwitches, currentSwitches);
        
        incrementOrCreate(CurrentWeaponSwitches, weapon.name, 1);
        maxOrCreate(MaxWeaponSwitches, weapon.name, CurrentWeaponSwitches[weapon.name]);
        incrementOrCreate(LifetimeWeaponSwitches, weapon.name, 1);
        maxOrCreate(MaxWeaponUsedAtLevel, weapon.name, FindObjectOfType<Difficulty>().getDifficulty());
    }

    private void SaveStats()
    {

    }

    private void LoadStats()
    {
        initFromClean();
    }

    public void initFromClean()
    {
        CurrentEnemykills = new Dictionary<string, Dictionary<string,int>>();
        CurrentWeaponKills = new Dictionary<string, int>();
        CurrentWeaponScores = new Dictionary<string, int>();
        CurrentPowerUps = new Dictionary<string, int>();

        MaxEnemyWeaponKills = new Dictionary<string, Dictionary<string, int>>();
        MaxEnemyKills = new Dictionary<string, int>();
        MaxWeaponKills = new Dictionary<string, int>();
        MaxWeaponScores = new Dictionary<string, int>();
        MaxPowerUps = new Dictionary<string, int>();

        LifetimeEnemyKills = new Dictionary<string, Dictionary<string, int>>();
        LifetimeWeaponKills = new Dictionary<string, int>();
        LifetimeWeaponScores = new Dictionary<string, int>();
            
        MaxWeaponSwitches = new Dictionary<string, int>();
        CurrentWeaponSwitches = new Dictionary<string, int>();
        LifetimeWeaponSwitches = new Dictionary<string, int>();

        MaxWeaponUsedAtLevel = new Dictionary<string, int>();
    }
    private void incrementOrCreate(Dictionary<string,int> dict, string str, int inc, string key2 = "")
    {
        if (!dict.ContainsKey(str))
            dict[str] = 0;
        dict[str] += inc;
    }
    private void incrementOrCreate(Dictionary<string, Dictionary<string,int>> dict, string str, string key2, int inc)
    {
        if (!dict.ContainsKey(str))
            dict[str] = new Dictionary<string, int>();
        incrementOrCreate(dict[str],key2,inc);
    }
    private void maxOrCreate(Dictionary<string, int> dict, string str, int val)
    {
        if (!dict.ContainsKey(str))
            dict[str] = 0;
        dict[str] = Mathf.Max(val,dict[str]);
    }
    private void maxOrCreate(Dictionary<string, Dictionary<string, int>> dict, string str, string key2, int val)
    {
        if (!dict.ContainsKey(str))
            dict[str] = new Dictionary<string, int>();
        maxOrCreate(dict[str], key2, val);
    }
    public void TrackKill(Score killedObject, WeaponStats originWeapon)
    {
        if (Disabled)
            return;
        currentScore += (int)killedObject.scoreValue;
        currentKills++;
        maxScore = Mathf.Max(currentScore, maxScore);
        maxKills = Mathf.Max(maxKills, currentKills);
        incrementOrCreate(CurrentEnemykills, killedObject.TrackName, originWeapon.name,1);
        incrementOrCreate(CurrentWeaponKills, originWeapon.name, 1);
        incrementOrCreate(CurrentWeaponScores, originWeapon.name, (int)killedObject.scoreValue);

        maxOrCreate(MaxEnemyWeaponKills, killedObject.TrackName, originWeapon.name, CurrentEnemykills[killedObject.TrackName][originWeapon.name]);
        maxOrCreate(MaxEnemyKills, killedObject.TrackName, SumStat(CurrentEnemykills[killedObject.TrackName]));
        maxOrCreate(MaxWeaponKills, originWeapon.name, CurrentWeaponKills[originWeapon.name]);
        maxOrCreate(MaxWeaponScores, originWeapon.name, CurrentWeaponScores[originWeapon.name]);

        incrementOrCreate(LifetimeEnemyKills, killedObject.TrackName,originWeapon.name, 1);
        incrementOrCreate(LifetimeWeaponKills, originWeapon.name, 1);
        incrementOrCreate(LifetimeWeaponScores, originWeapon.name, (int)killedObject.scoreValue);
    }

    public void TrackPowerUp(string powerUpID)
    {
        if (Disabled)
            return;
        incrementOrCreate(CurrentPowerUps, powerUpID, 1);
        maxOrCreate(MaxPowerUps, powerUpID, CurrentPowerUps[powerUpID]);
    }
    private void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        CurrentEnemykills = new Dictionary<string, Dictionary<string, int>>();
        CurrentWeaponKills = new Dictionary<string, int>();
        CurrentWeaponScores = new Dictionary<string, int>();
        CurrentPowerUps = new Dictionary<string, int>();
        CurrentWeaponSwitches = new Dictionary<string, int>();
        currentScore = 0;
        currentKills = 0;
        currentSwitches = 0;
        Disabled = (SceneManager.GetActiveScene().name == "TempTitle");
    }

    public int SumStat(Dictionary<string, Dictionary<string,int>> list)
    {
        int n = 0;
        foreach (Dictionary<string, int> i in list.Values)
            n += SumStat(i);
        return n;
    }
    public int SumStat(Dictionary<string,int> list)
    {
        int n = 0;
        foreach (int i in list.Values)
            n += i;
        return n;
    }

    public int MaxVal(Dictionary<string, int> list)
    {
        int n = 0;
        foreach (int i in list.Values)
            n = Mathf.Max(n, i);
        return n;
    }
    public int GetVal(Dictionary<string, int> list, string key)
    {
        if (!list.ContainsKey(key))
        {
            list[key] = 0;
            return 0;
        }
        return list[key];
    }
    public string MaxValString(Dictionary<string, int> list)
    {
        int n = 0;
        string ret = "";
        foreach (string s in list.Keys)
        {
            int newVal = list[s];
            if (newVal > n)
            {
                n = newVal;
                ret = s;
            }
        }
        return ret;
    }
    public SaveObject TransferToSaveObject()
    {
        SaveObject newSave = new SaveObject
        {

            savLifetimeEnemyKills = LifetimeEnemyKills,
            savLifetimeWeaponKills = LifetimeWeaponKills,
            savLifetimeWeaponScores = LifetimeWeaponScores,
            savLifetimeWeaponSwitches = LifetimeWeaponSwitches,

            savMaxEnemyWeaponKills = MaxEnemyWeaponKills,
            savMaxEnemykills = MaxEnemyKills,
            savmaxScore = maxScore,
            savmaxKills = maxKills,
            savmaxSwitches = maxSwitches,
            savMaxWeaponKills = MaxWeaponKills,
            savMaxWeaponScores = MaxWeaponScores,
            savMaxWeaponSwitches = MaxWeaponSwitches,
            savMaxWeaponUsedAtLevel = MaxWeaponUsedAtLevel,
            savMaxPowerups = MaxPowerUps,
            savAchivementsDone = FindObjectOfType<AchievementManager>().AchievementsAlreadyUnlocked
        };


        return newSave;
    }

    public void LoadFromSaveObject(SaveObject oldSave)
    {
        LifetimeEnemyKills = oldSave.savLifetimeEnemyKills;
        LifetimeWeaponKills = oldSave.savLifetimeWeaponKills;
        LifetimeWeaponScores = oldSave.savLifetimeWeaponScores;
        LifetimeWeaponSwitches = oldSave.savLifetimeWeaponSwitches;

        MaxEnemyKills = oldSave.savMaxEnemykills;
        maxScore = oldSave.savmaxScore;
        MaxEnemyWeaponKills = oldSave.savMaxEnemyWeaponKills;
        maxKills = oldSave.savmaxKills;
        maxSwitches = oldSave.savmaxSwitches;
        MaxWeaponKills = oldSave.savMaxWeaponKills;
        MaxWeaponScores = oldSave.savMaxWeaponScores;
        MaxWeaponSwitches = oldSave.savMaxWeaponSwitches;
        MaxWeaponUsedAtLevel = oldSave.savMaxWeaponUsedAtLevel;
        MaxPowerUps = oldSave.savMaxPowerups;
        FindObjectOfType<AchievementManager>().AchievementsAlreadyUnlocked = oldSave.savAchivementsDone;
    }

    public bool IsAchievementMet(Dictionary<string,int> d,string key, int criteria)
    {
        if (d.ContainsKey(key))
            return d[key] >= criteria;
        else
            d[key] = 0;
        return false;
    }
    public bool IsAchievementMet(Dictionary<string, Dictionary<string,int>> d, string key,string key2, int criteria)
    {
        if (d.ContainsKey(key) && d[key].ContainsKey(key2))
            return d[key][key2] >= criteria;
        else
            d[key] = new Dictionary<string, int>();
        return false;
    }
    public bool IsAchievementMet(Dictionary<string, Dictionary<string, int>> d, string key, int criteria)
    {
        if (d.ContainsKey(key) )
            return SumStat(d[key]) >= criteria;
        else
            d[key] = new Dictionary<string, int>();
        return false;
    }


    public void resetAll()
    {

    }
}
