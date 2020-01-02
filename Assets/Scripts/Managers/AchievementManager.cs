using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private static AchievementManager m_instance;
    public GameObject UnlockNotificationPrefab;
    public List<string> AchievementsAlreadyUnlocked;
    public bool UnlockEverythingMode = false;
    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;  
            //Debug.Log("List looks like null, trying to make a new list");
            //Debug.Log(AchievementsAlreadyUnlocked.ToString());
        }
        else if (m_instance != this)
        {
            Debug.Log("Function 2");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AchievementsAlreadyUnlocked == null)
        {
            AchievementsAlreadyUnlocked = new List<string>();
        }
    }

    public bool CheckIfAchievementIsMet(Achievement a, Transform notificationTransform=null)
    {
        //Debug.Log(AchievementsAlreadyUnlocked);
        //Debug.Log(a.DisplayName);
        //Debug.Log(AchievementsAlreadyUnlocked[0]);
        if (UnlockEverythingMode)
            return true;
        if (AchievementsAlreadyUnlocked.Contains(a.DisplayName))
        {
            return a;
        }
        else
        {
            bool met = a.CheckAchievementMet();
            if (met)
            {
                AchievementsAlreadyUnlocked.Add(a.DisplayName);
                if (notificationTransform != null)
                {
                    GameObject g = Instantiate(UnlockNotificationPrefab, notificationTransform);
                    string d = (a.Description.Length > 0) ? a.Description : a.AutoGenDescription();
                    g.GetComponent<UnlockNotification>().SetInfo(a.GetComponent<WeaponStats>().Icon, a.GetComponent<WeaponStats>().name, a.DisplayName, d);
                }

            }

            return met;
        }
    }

    public void ResetAchievements()
    {
        AchievementsAlreadyUnlocked.Clear();
    }

}
