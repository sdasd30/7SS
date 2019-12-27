using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadStatScreen()
    {
        SceneManager.LoadScene("Statistics");
    }

}
