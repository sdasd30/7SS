using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    Slider mSlider;
    Text mText;
    public string dispText;
    // Start is called before the first frame update
    void Start()
    {
        mSlider = GetComponentInParent<Slider>();
        mText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string thing = ((mSlider.value + 45f) * 2f).ToString("0");
        Debug.Log(thing);
        mText.text = (dispText + thing);
    }
}
