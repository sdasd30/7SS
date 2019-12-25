using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static GameManager m_instance;

    void Awake()
    {

        if (m_instance == null)
        {
            m_instance = this;
            //SceneManager.sceneLoaded += InitCanvasOnSceneLoad;
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
}
