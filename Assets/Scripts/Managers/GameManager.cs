using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

[RequireComponent(typeof(CameraFollow))]
public class GameManager : MonoBehaviour
{
    public GameObject CurrentPlayer;
    public Text GameOverText;
    public GameObject LoseMenu;
    private bool m_startedGameOver = false;
    private float m_timeSinceGameOver = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        findATarget();
        if (CurrentPlayer != null)
        {
            setPlayerCharacter(CurrentPlayer);
        }
        WriteSaveToFile();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentPlayer == null)
            m_startedGameOver = true;
        if (m_startedGameOver)
            gameOverSequence();
    }

    void gameOverSequence()
    {
        Time.timeScale = Mathf.Max(0f, 1f - (m_timeSinceGameOver / 2f));
        m_timeSinceGameOver += Time.unscaledDeltaTime;
        GameOverText.color = new Color(1f, 1f, 1f, Mathf.Max(0f, Mathf.Min(1f, m_timeSinceGameOver - 1f)));
        if (m_timeSinceGameOver > 3f)
        {
            Time.timeScale = 1.0f;
            LoseMenu.SetActive(true);
        }
    }
    private void findATarget()
    {
        BasicMovement[] ListMovements = FindObjectsOfType<BasicMovement>();
        foreach (BasicMovement bm in ListMovements)
        {
            if (bm.IsCurrentPlayer)
            {
                CurrentPlayer = bm.gameObject;
                return;
            }
        }
    }
    private void setPlayerCharacter(GameObject go)
    {
        GetComponent<CameraFollow>().SetPlayerObj(go.transform);
        GetComponentInChildren<CurrentWeaponUI>().init(go);
        GetComponentInChildren<TimeRemaining>().init(go);
        GetComponentInChildren<HPSlider>().init(go);
        GetComponentInChildren<HPBoxes>().init(go);
        GetComponentInChildren<NextWeaponUI>().init(go);
        GetComponentInChildren<HPText>().init(go);
        FindObjectOfType<StatTracker>().SetPlayerObj(go);
    }

    public void RetryLevel()
    {
        WriteSaveToFile();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<CardManager>().resetWeapons();
    }

    public void WeaponSelection()
    {
        WriteSaveToFile();
        Time.timeScale = 1f;
        SceneManager.LoadScene("CardSelection");
    }
    public void TitleScreen()
    {
        WriteSaveToFile();
        Time.timeScale = 1f;
        SceneManager.LoadScene("TempTitle");
    }
    // Update is called once per frame
    public void WriteSaveToFile()
    {
        if (FindObjectOfType<StatTracker>() == null)
            return;
        SaveObject newSave = FindObjectOfType<StatTracker>().TransferToSaveObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "\\gamesave.sav"); //
        Debug.Log("Saved Game to " + Application.persistentDataPath + "\\gamesave.sav");
        bf.Serialize(file, newSave);
        file.Close();

        Debug.Log("Game Saved");
    }
}
