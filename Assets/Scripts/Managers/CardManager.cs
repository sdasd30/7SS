using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    private static CardManager m_instance;

    public List<GameObject> CurrentHand = new List<GameObject>();
    void Awake()
    {

        if (m_instance == null)
        {
            m_instance = this;
            SceneManager.sceneLoaded += onSceneLoad;
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
    private void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        WeaponHandler wh = FindObjectOfType<WeaponHandler>();
        if (scene.name != "Tutorial")
        {
            if (wh != null)
            {
                wh.weapons = CurrentHand;
            }
        }
    }

    public void resetWeapons()
    {
        WeaponHandler wh = FindObjectOfType<WeaponHandler>();
        if (wh != null)
        {
            wh.weapons = CurrentHand;
        }
    }

}
