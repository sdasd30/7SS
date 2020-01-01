using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedPowerSlider : MonoBehaviour
{
    WeaponHandler plWH; // PLayer's Basic Movement
    Slider mSlider;

    // Start is called before the first frame update
    void Start()
    {
        BasicMovement[] ListMovements = FindObjectsOfType<BasicMovement>();
        mSlider = GetComponent<Slider>();
        foreach (BasicMovement bm in ListMovements)
        {
            if (bm.IsCurrentPlayer)
            {
                plWH = bm.gameObject.GetComponentInChildren<WeaponHandler>();
                return;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        mSlider.value = -plWH.ReturnPowerCooldownSpeed();
    }
}
