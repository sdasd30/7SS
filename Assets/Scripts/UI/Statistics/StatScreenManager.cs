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
        disp += "Most Enemies Defeated : " + m_st.maxKills + "  \t \t \t| Most Weapon Kills (one life): " + m_st.MaxValString(m_st.MaxWeaponKills) + " : " + m_st.MaxVal(m_st.MaxWeaponKills) + "\n";
        disp += "Total Enemies Defeated : " + m_st.SumStat(m_st.LifetimeEnemyKills) + "   \t \t \t| Most Weapon Kills (all time): " + m_st.MaxValString(m_st.LifetimeWeaponKills) + " : " + m_st.MaxVal(m_st.LifetimeWeaponKills) + "\n";
        disp += "Highest Score : " + m_st.maxScore + "     \t \t \t \t \t \t| Highest Score Weapon: " + m_st.MaxValString(m_st.MaxWeaponScores) + " : " + m_st.MaxVal(m_st.MaxWeaponScores) + " points \n";
        disp += "Most Switches (one life) : " + m_st.maxSwitches + "   \t \t \t| Most Used Weapon: " + m_st.MaxValString(m_st.LifetimeWeaponSwitches) + " : " + m_st.MaxVal(m_st.LifetimeWeaponSwitches) + " times \n";
        disp += "Highest Level : " + m_st.MaxVal(m_st.MaxWeaponUsedAtLevel) + "\n";
        StatDisplay.text = disp;
    }

    public void ShowCardStats(StatCard sc)
    {
        string disp = "";
        string ws = sc.Weapon.GetComponent<WeaponStats>().name;
        disp += "Most Kills in One Game: " + m_st.GetVal(m_st.MaxWeaponKills,ws) + "   \t \t \t| Highest Score in One Game: " + m_st.GetVal(m_st.MaxWeaponScores,ws) + "\n";
        disp += "Lifetime Kills: " + m_st.GetVal(m_st.LifetimeWeaponKills,ws) + "       \t \t \t \t \t \t \t| Lifetime Score: " + m_st.GetVal(m_st.LifetimeWeaponScores,ws) + "\n";
        disp += "Most Uses (one game): " + m_st.GetVal(m_st.MaxWeaponSwitches,ws) + "    \t \t \t \t| Total Times Used: " + m_st.GetVal(m_st.LifetimeWeaponSwitches,ws) + "\n";
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
                AddCardPrefix(newCard);
                m_poolObjs.Add(newCard);
                insertCard(newCard.transform, PoolTransform, newCard.gameObject.name);
                PoolTransform.sizeDelta = new Vector2(PoolTransform.rect.width, (32 + (Mathf.Ceil(m_poolObjs.Count / 7f)) * 136));
            }
        }
    }
    private void insertCard(Transform child, Transform newParent, string name)
    {
        for (int i = 0; i < newParent.childCount; i++)
        {
            if (name.CompareTo(newParent.GetChild(i).gameObject.name) < 0)
            {
                child.SetParent(newParent);
                child.SetSiblingIndex(i);
                return;
            }
        }
        child.SetParent(newParent);
    }
    private void AddCardPrefix(GameObject go)
    {
        if (go.GetComponent<Achievement>() && !m_AchievementManager.CheckIfAchievementIsMet(go.GetComponent<Achievement>(), transform))
        {
            go.name = "9" + go.GetComponent<StatCard>().Weapon.GetComponent<WeaponStats>().name;
            return;
        }
        int i = go.GetComponent<StatCard>().Weapon.GetComponent<WeaponStats>().Cost + 1;
        go.name = i + go.GetComponent<StatCard>().Weapon.GetComponent<WeaponStats>().name;
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
