using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatScreenManager : MonoBehaviour
{
    public AchievementManager m_AchievementManager;
    public StatTracker m_st;
    public Text StatDisplay;
    public GameObject CardPrefab;
    public RectTransform PoolTransform;

    private List<GameObject> m_poolObjs;

    // Start is called before the first frame update
    void Start()
    {
        m_poolObjs = new List<GameObject>();
        if (m_AchievementManager == null)
            m_AchievementManager = FindObjectOfType<AchievementManager>();
        if (m_st == null)
            m_st = FindObjectOfType<StatTracker>();

        initializePool();
        ShowOverallStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //public Dictionary<string, Dictionary<string, int>> CurrentEnemykills;
    //public Dictionary<string, int> CurrentWeaponKills;
    //public Dictionary<string, int> CurrentWeaponScores;
    //public Dictionary<string, int> CurrentPowerUps;

    //public Dictionary<string, Dictionary<string, int>> MaxEnemyWeaponKills;
    //public Dictionary<string, int> MaxEnemyKills;
    //public Dictionary<string, int> MaxWeaponKills;
    //public Dictionary<string, int> MaxWeaponScores;

    //public Dictionary<string, Dictionary<string, int>> LifetimeEnemyKills;
    //public Dictionary<string, int> LifetimeWeaponKills;
    //public Dictionary<string, int> LifetimeWeaponScores;

    //public Dictionary<string, int> MaxWeaponSwitches;
    //public Dictionary<string, int> LifetimeWeaponSwitches;

    //public Dictionary<string, int> MaxWeaponUsedAtLevel;


    //public Dictionary<string, int> MaxPowerUps;
    public void ShowOverallStats()
    {
        string disp = "";
        disp += "Most Enemies Defeated: " + m_st.maxKills + " | Most Weapon Switches: " + m_st.maxSwitches + "\n";
        disp += "Highest Score: " + m_st.maxScore  + " | Most Used Weapon: " + m_st.MaxValString(m_st.LifetimeWeaponKills) + "\n";
        disp += "Total Enemies Defeated: " + m_st.SumStat(m_st.LifetimeEnemyKills) + "\n";
        disp += "Most Weapon Switches: " + m_st.maxSwitches + "\n";
        disp += "Highest Level: " + m_st.MaxVal(m_st.MaxWeaponUsedAtLevel) + "\n";
        StatDisplay.text = disp;
    }

    public void ShowCardStats(StatCard sc)
    {
        string disp = "";
        string ws = sc.Weapon.GetComponent<WeaponStats>().name;
        disp += "Most Kills in One Game: " + m_st.GetVal(m_st.MaxWeaponKills,ws) + " | Highest Score in One Game: " + m_st.GetVal(m_st.MaxWeaponScores,ws) + "\n";
        disp += "Lifetime Kills: " + m_st.GetVal(m_st.LifetimeWeaponKills,ws) + " | Lifetime Score: " + m_st.GetVal(m_st.LifetimeWeaponScores,ws) + "\n";
        disp += "Most Times Used in One Game: " + m_st.GetVal(m_st.MaxWeaponSwitches,ws) + " | Total Times Used: " + m_st.GetVal(m_st.LifetimeWeaponSwitches,ws) + "\n";
        disp += "Highest Level: " + m_st.GetVal(m_st.MaxWeaponUsedAtLevel,ws) + "\n";
        StatDisplay.text = disp;
    }
    private void initializePool()
    {
        Object[] loadedObjs = Resources.LoadAll("Weapons", typeof(GameObject));
        foreach (Object o in loadedObjs)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent<WeaponStats>() != null)
            {
                if (g.GetComponent<Achievement>() && !m_AchievementManager.CheckIfAchievementIsMet(g.GetComponent<Achievement>(), transform))
                {
                    continue;
                }
                GameObject newCard = createCard(g);
                m_poolObjs.Add(newCard);
                newCard.transform.SetParent(PoolTransform);
                PoolTransform.sizeDelta = new Vector2(PoolTransform.rect.width, (32 + (Mathf.Ceil(m_poolObjs.Count / 7f)) * 136));
            }
        }
    }
    private GameObject createCard(GameObject wi)
    {
        GameObject gi = Instantiate(CardPrefab);
        WeaponStats wp = wi.GetComponent<WeaponStats>();
        gi.GetComponent<StatCard>().SetCardInfo(wp.Icon, wp.name, wp.Cost, wi);
        gi.GetComponent<StatCard>().UIManager = this;
        return gi;
    }
}
