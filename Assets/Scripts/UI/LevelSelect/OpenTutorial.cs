using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenTutorial : MonoBehaviour
{
    public void openScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
