using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CameraFollow))]
public class GameManager : MonoBehaviour
{
    public GameObject CurrentPlayer;
    public Text GameOverText;
    private bool m_startedGameOver = false;
    private float m_timeSinceGameOver = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        findATarget();
        if (CurrentPlayer != null)
            GetComponent<CameraFollow>().SetPlayerObj(CurrentPlayer.transform);
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
        if (m_timeSinceGameOver > 10.0f)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            }
        }
    }
}
