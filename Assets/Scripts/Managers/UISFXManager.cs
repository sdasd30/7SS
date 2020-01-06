using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UISFXManager : MonoBehaviour
{

    public AudioClip ButtonHighlight;
    public AudioClip ButtonClick;
    private static UISFXManager m_instance;

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
    private void onSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Button[] listButtons = FindObjectsOfType<Button>();
        foreach(Button b in listButtons)
        {
            if (b.GetComponent<UISFXManager>() == null)
            {
                b.gameObject.AddComponent<ButtonSFX>();
                b.GetComponent<ButtonSFX>().ButtonHighlight = ButtonHighlight;
                b.GetComponent<ButtonSFX>().ButtonClick = ButtonClick;
            }
        }
    }
}
