using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour
{
    Toggle mToggle;
    private void Start()
    {
        mToggle = GetComponent<Toggle>();
    }
    public void ToggleFullscreen()
    {
        Screen.fullScreen = mToggle.isOn;
    }
}
