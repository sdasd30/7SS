using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixerGroup mixr;
    bool muted;
    float origins;
    private void Start()
    {
        mixr = FindObjectOfType<AudioMixerGroup>();
    }
    public void muteGame()
    {
        if (!muted)
        {
            mixr.audioMixer.SetFloat("MasterMixer", 0f);
            muted = true;
        }
        if (muted)
        {

        }
    }
}
