using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatTracker : MonoBehaviour
{
    private static StatTracker m_instance;
    public Dictionary<string, int> CurrentEnemykills;
    public Dictionary<string, int> CurrentWeaponKills;
    public Dictionary<string, int> CurrentWeaponScores;

    public Dictionary<string, int> MaxEnemykills;
    public Dictionary<string, int> MaxWeaponKills;
    public Dictionary<string, int> MaxWeaponScores;

    public Dictionary<string, int> LifetimeEnemykills;
    public Dictionary<string, int> LifetimeWeaponKills;
    public Dictionary<string, int> LifetimeWeaponScores;

    public Dictionary<string, int> MaxWeaponSwitches;
    public Dictionary<string, int> LifetimeWeaponSwitches;

    public Dictionary<string, int> MaxWeaponUsedAtLevel;

    public int currentScore;
    public int maxScore;
    public int currentKills;
    public int maxKills;
    public int currentSwitches;
    public int maxSwitches;

    private string m_currentWeaponName = "";
    private GameObject m_currentPlayer;
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
        currentSwitches++;
        maxSwitches = Mathf.Max(maxSwitches, currentSwitches);
        incrementOrCreate(MaxWeaponSwitches, weapon.name, 1);
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

    private void initFromClean()
    {
        CurrentEnemykills = new Dictionary<string, int>();
        CurrentWeaponKills = new Dictionary<string, int>();
        CurrentWeaponScores = new Dictionary<string, int>();

        MaxEnemykills = new Dictionary<string, int>();
        MaxWeaponKills = new Dictionary<string, int>();
        MaxWeaponScores = new Dictionary<string, int>();

        LifetimeEnemykills = new Dictionary<string, int>();
        LifetimeWeaponKills = new Dictionary<string, int>();
        LifetimeWeaponScores = new Dictionary<string, int>();

        MaxWeaponSwitches = new Dictionary<string, int>();
        LifetimeWeaponSwitches = new Dictionary<string, int>();

        MaxWeaponUsedAtLevel = new Dictionary<string, int>();
    }
    private void incrementOrCreate(Dictionary<string,int> dict, string str, int inc)
    {
        if (!dict.ContainsKey(str))
            dict[str] = 0;
        dict[str] += inc;
    }
    private void maxOrCreate(Dictionary<string, int> dict, string str, int val)
    {
        if (!dict.ContainsKey(str))
            dict[str] = 0;
        dict[str] = Mathf.Max(val,dict[str]);
    }

    public void TrackKill(Score killedObject, WeaponStats originWeapon)
    {
        currentScore += (int)killedObject.scoreValue;
        currentKills++;
        maxScore = Mathf.Max(currentScore, maxScore);
        maxKills = Mathf.Max(maxKills, currentKills);
        incrementOrCreate(CurrentEnemykills, killedObject.TrackName, 1);
        incrementOrCreate(CurrentWeaponKills, originWeapon.name, 1);
        incrementOrCreate(CurrentWeaponScores, originWeapon.name, (int)killedObject.scoreValue);

        maxOrCreate(MaxEnemykills, killedObject.TrackName, CurrentEnemykills[killedObject.TrackName]);
        maxOrCreate(MaxWeaponKills, originWeapon.name, CurrentWeaponKills[originWeapon.name]);
        maxOrCreate(MaxWeaponScores, originWeapon.name, CurrentWeaponScores[originWeapon.name]);

        incrementOrCreate(LifetimeEnemykills, killedObject.TrackName, 1);
        incrementOrCreate(LifetimeWeaponKills, originWeapon.name, 1);
        incrementOrCreate(LifetimeWeaponScores, originWeapon.name, (int)killedObject.scoreValue);
    }

    private void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        CurrentEnemykills = new Dictionary<string, int>();
        CurrentWeaponKills = new Dictionary<string, int>();
        CurrentWeaponScores = new Dictionary<string, int>();
        currentScore = 0;
        currentKills = 0;
        currentSwitches = 0;
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
    public SaveObject TransferToSaveObject()
    {
        SaveObject newSave = new SaveObject
        {
            savLifetimeEnemykills = LifetimeEnemykills,
            savLifetimeWeaponKills = LifetimeWeaponKills,
            savLifetimeWeaponScores = LifetimeWeaponScores,
            savLifetimeWeaponSwitches = LifetimeWeaponSwitches,

            savMaxEnemykills = MaxEnemykills,
            savmaxScore = maxScore,
            savmaxKills = maxKills,
            savmaxSwitches = maxSwitches,
            savMaxWeaponKills = MaxWeaponKills,
            savMaxWeaponScores = MaxWeaponScores,
            savMaxWeaponSwitches = MaxWeaponSwitches,
            savMaxWeaponUsedAtLevel = MaxWeaponUsedAtLevel,
            savAchivementsDone = FindObjectOfType<AchievementManager>().AchievementsAlreadyUnlocked
        };


        return newSave;
    }

    public void LoadFromSaveObject(SaveObject oldSave)
    {
        LifetimeEnemykills = oldSave.savLifetimeEnemykills;
        LifetimeWeaponKills = oldSave.savLifetimeWeaponKills;
        LifetimeWeaponScores = oldSave.savLifetimeWeaponScores;
        LifetimeWeaponSwitches = oldSave.savLifetimeWeaponSwitches;

        MaxEnemykills = oldSave.savMaxEnemykills;
        maxScore = oldSave.savmaxScore;
        maxKills = oldSave.savmaxKills;
        maxSwitches = oldSave.savmaxSwitches;
        MaxWeaponKills = oldSave.savMaxWeaponKills;
        MaxWeaponScores = oldSave.savMaxWeaponScores;
        MaxWeaponSwitches = oldSave.savMaxWeaponSwitches;
        MaxWeaponUsedAtLevel = oldSave.savMaxWeaponUsedAtLevel;
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
}
