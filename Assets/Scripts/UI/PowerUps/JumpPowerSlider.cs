using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpPowerSlider : MonoBehaviour
{
    BasicMovement plBM; // PLayer's Basic Movement
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
                plBM = bm.gameObject.GetComponent<BasicMovement>();
                return;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        mSlider.value = -plBM.returnPowerUpCooling();
    }
}
