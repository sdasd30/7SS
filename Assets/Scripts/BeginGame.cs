using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    public void LoadTestScene()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    /*
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */
}
