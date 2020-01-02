using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectUI : MonoBehaviour
{
    public List<LevelCard> AllLevelCards;
    public GameObject CardPrefab;
    public RectTransform MaskTransform;
    public Text LevelName;
    public Text LevelDescription;
    public Button StartLevelButton;
    public Scrollbar Scroll;

    public LevelCard CurrentSelectedCard;
    private int m_lastHighlightedCard;
    // Start is called before the first frame update
    void Start()
    {
        initializeCards();
        HighlightCard(AllLevelCards[0]);
        m_lastHighlightedCard = 0;
        Scroll.value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void initializeCards()
    {
        Object[] loadedObjs = Resources.LoadAll("LevelCards", typeof(GameObject));
        foreach (Object o in loadedObjs)
        {
            GameObject g = (GameObject)o;
            GameObject newCard = Instantiate(g, MaskTransform);
            AllLevelCards.Add(newCard.GetComponent<LevelCard>());
            MaskTransform.sizeDelta = new Vector2( 64 + (AllLevelCards.Count * 448), MaskTransform.rect.height);
        }
    }
    public void OnScroll(Vector2 scroll)
    {
        int i = CalculatedHighlishtedCard(scroll);
        if (i != m_lastHighlightedCard && i <= AllLevelCards.Count)
        {
            m_lastHighlightedCard = i;
            HighlightCard(AllLevelCards[i]);
        }
    }

    private int CalculatedHighlishtedCard(Vector2 scroll)
    {
        int numSelections = AllLevelCards.Count;
        float sel = (1.0f / numSelections);
        int cardID = Mathf.FloorToInt(scroll.x / sel);
        //Debug.Log(scroll.x + " : " + cardID + " : " + numSelections + " : " + (scroll.x / sel));
        return Mathf.Max(0,Mathf.Min(numSelections - 1, cardID));
    }
    private void HighlightCard(LevelCard lc)
    {
        LevelName.text = lc.NameOfLevel;
        LevelDescription.text = lc.Description;
        CurrentSelectedCard = lc;
    }
    public void OnGameStart()
    {
        if (CurrentSelectedCard != null)
        {
            FindObjectOfType<LevelManager>().LastLevelSelected = CurrentSelectedCard.AssociatedSceneName;
            SceneManager.LoadScene("CardSelection");
        }
            
    }
}
