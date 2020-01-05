using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    float audioLevel;
    public AudioMixer mixr;
    public string modifiying;
    Slider mSlider;
    // Start is called before the first frame update
    private void Start()
    {
        mSlider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update()
    {
        if (mSlider.value != -45)
        {
            mixr.SetFloat(modifiying, mSlider.value);
        }
        else
            mixr.SetFloat(modifiying, -999);
    }

    public float GetLevel()
    {
        return mSlider.value;
    }

    public void SetLevel(float audio)
    {
        mSlider.value = audio;
    }
}
