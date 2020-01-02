using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CardUIManager : MonoBehaviour
{
    public CardManager m_CardManager;
    public AchievementManager m_AchievementManager;
    public Transform HandTransform;
    public GameObject CardPrefab;
    public RectTransform PoolTransform;

    public Text PointCounter;
    public Text ExtraMessage;
    public Button StartGameButton;



    private int CardTarget = 7;
    private int MaxCost = 7;

    private List<GameObject> m_poolObjs;
    private List<GameObject> m_handObjs;

    // Start is called before the first frame update
    void Start()
    {
        m_handObjs = new List<GameObject>();
        m_poolObjs = new List<GameObject>();
        if (m_CardManager == null)
            m_CardManager = FindObjectOfType<CardManager>();
        if (m_CardManager == null)
            return;
        if (m_AchievementManager == null)
            m_AchievementManager = FindObjectOfType<AchievementManager>();
        initializeHands();
        initializePool();
        UpdateButton(CheckValid());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeHands()
    {
        foreach (GameObject go in m_CardManager.CurrentHand)
        {
            GameObject newCard = createCard(go);
            AddCardPrefix(newCard);
            newCard.GetComponent<Card>().InHand = true;
            m_handObjs.Add(newCard);
            newCard.transform.SetParent(HandTransform);
        }
    }
    private void initializePool()
    {
        Object[] loadedObjs = Resources.LoadAll("Weapons", typeof(GameObject));
        foreach(Object o in loadedObjs)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent<WeaponStats>() != null)
            {
                if (inHand(g.GetComponent<WeaponStats>()))
                    continue;
                if (g.GetComponent<Achievement>() && !m_AchievementManager.CheckIfAchievementIsMet(g.GetComponent<Achievement>(),transform)) {
                    GameObject lockCard = createLockCard(g.GetComponent<Achievement>(),g);
                    AddCardPrefix(lockCard);
                    m_poolObjs.Add(lockCard);
                    insertCard(lockCard.transform, PoolTransform, lockCard.gameObject.name);
                    PoolTransform.sizeDelta = new Vector2(PoolTransform.rect.width, (32 + (Mathf.Ceil(m_poolObjs.Count / 7f)) * 136));
                    continue;
                }
                GameObject newCard = createCard(g);
                AddCardPrefix(newCard);
                m_poolObjs.Add(newCard);
                insertCard(newCard.transform, PoolTransform, newCard.gameObject.name);
                PoolTransform.sizeDelta = new Vector2 (PoolTransform.rect.width, (32 + (Mathf.Ceil(m_poolObjs.Count / 7f)) * 136));
            }
        }
    }
    private bool inHand(WeaponStats ws)
    {
        foreach(GameObject go in m_handObjs)
        {
            if (go.GetComponent<Card>().Weapon.GetComponent<WeaponStats>().name == ws.name)
                return true;
        }
        return false;
    }
    private void AddCardPrefix(GameObject card)
    {
        GameObject w = card.GetComponent<Card>().Weapon;
        if (w.GetComponent<Achievement>() && !m_AchievementManager.CheckIfAchievementIsMet(w.GetComponent<Achievement>(), transform))
        {
            card.name = "9" + w.GetComponent<WeaponStats>().name;
            return;
        }
        int i = w.GetComponent<WeaponStats>().Cost + 1;
        card.name = i + w.GetComponent<WeaponStats>().name;
    }
    public void SwapCard(Card c)
    {
        if (c.InHand)
        {
            
            m_handObjs.Remove(c.gameObject);
            m_poolObjs.Add(c.gameObject);

            insertCard(c.transform, PoolTransform, c.gameObject.name);
        } else
        {
            if (m_handObjs.Count >= 7)
                return;
            m_poolObjs.Remove(c.gameObject);
            m_handObjs.Add(c.gameObject);
            c.transform.SetParent(HandTransform);
            
        }
        c.InHand = !c.InHand;
        UpdateButton(CheckValid());
    }
    
    private void insertCard( Transform child, Transform newParent, string name)
    {
        for (int i = 0; i < newParent.childCount; i++)
        {
            if (name.CompareTo(newParent.GetChild(i).gameObject.name) < 0 ) {
                child.SetParent(newParent);
                child.SetSiblingIndex(i);
                return;
            }
        }
        child.SetParent(newParent);
    }
    private bool CheckValid()
    {
        bool valid = true;
        if (m_handObjs.Count != CardTarget)
        {
            ExtraMessage.text = "Please Select 7 Cards to Start";
            valid = false;
        } else
        {
            ExtraMessage.text = "Ready to Start Game!";
        }
        int cost = 0;
        foreach(GameObject g in m_handObjs)
        {
            cost += g.GetComponent<Card>().Weapon.GetComponent<WeaponStats>().Cost;
        }
        if (cost > MaxCost)
        {
            ExtraMessage.text = "You are Over Budget!";
            valid = false;
        }
        PointCounter.text = "Points Used: " + cost + " / " + MaxCost;
        return valid;
    }
    private void UpdateButton(bool ready)
    {
        StartGameButton.interactable = ready;
    }
    private GameObject createCard(GameObject wi)
    {
        GameObject gi = Instantiate(CardPrefab);
        WeaponStats wp = wi.GetComponent<WeaponStats>();
        gi.GetComponent<Card>().SetCardInfo(wp.Icon, wp.name, wp.Cost,wi);
        gi.GetComponent<Card>().UIManager = this;
        return gi;
    }
    private GameObject createLockCard(Achievement a, GameObject weapon)
    {
        GameObject gi = Instantiate(CardPrefab);
        if (a.Description.Length == 0)
            gi.GetComponent<Card>().SetLockCardInfo(a.DisplayName, a.AutoGenDescription(), weapon);
        else
            gi.GetComponent<Card>().SetLockCardInfo(a.DisplayName,a.Description, weapon);
        gi.GetComponent<Card>().UIManager = this;
        return gi;
    }
    public void InitializeGame()
    {
        List<GameObject> finalHand = new List<GameObject>();
        foreach (GameObject go in m_handObjs)
        {
            finalHand.Add(go.GetComponent<Card>().Weapon);
        }
        m_CardManager.CurrentHand = finalHand;
        if (FindObjectOfType<LevelManager>() != null)
            SceneManager.LoadScene(FindObjectOfType<LevelManager>().LastLevelSelected);
        else
            SceneManager.LoadScene("SampleScene");
    }
}
