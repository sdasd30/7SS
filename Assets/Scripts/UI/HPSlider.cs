using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    public GameObject player;
    Attackable plr;
    RectTransform fillArea;
    Slider kk;
    int sliderMax;
    float sliderCurrent;
    // Start is called before the first frame update
    void Start()
    {
        fillArea = GetComponentInChildren<RectTransform>();
        plr = player.GetComponent<Attackable>();
        kk = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) updateSlider();
    }

    private void updateSlider()
    {
        sliderMax = (int)plr.maxHP;
        sliderCurrent = plr.hp;
        kk.maxValue = sliderMax;
        kk.value = sliderCurrent;
        fillArea.offsetMax = new Vector2(((plr.maxHP * 64f)), fillArea.offsetMax.y);
    }
}
