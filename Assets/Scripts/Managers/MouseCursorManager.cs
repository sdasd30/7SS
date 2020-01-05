using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseCursorManager : MonoBehaviour
{
    public List<string> MenuSceneNames;
    private static MouseCursorManager m_instance;
    public Texture2D MenuMouse;
    public Texture2D ActionMouse;
    public CursorMode cursorMode = CursorMode.Auto;

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
        string sceneName = scene.name;
        if (MenuSceneNames.Contains(sceneName)) {
            Vector2 hspt = Vector2.zero;
            Cursor.SetCursor(MenuMouse, hspt, cursorMode);
        } else
        {
            Cursor.SetCursor(ActionMouse, new Vector2(ActionMouse.width / 2, ActionMouse.height / 2), cursorMode);
        }
    }
}
