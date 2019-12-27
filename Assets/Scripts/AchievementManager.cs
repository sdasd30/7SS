using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager m_instance;

    public List<string> AchievementsAlreadyUnlocked;
    void Awake()
    {

        if (m_instance == null)
        {
            m_instance = this;
        }
        else if (m_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckIfAchievementIsMet(Achievement a)
    {
        if (AchievementsAlreadyUnlocked.Contains(a.DisplayName))
            return a;
        else
        {
            bool met = a.CheckAchievementMet();
            if (met)
                AchievementsAlreadyUnlocked.Add(a.DisplayName);
            return met;
        }
    }
}
