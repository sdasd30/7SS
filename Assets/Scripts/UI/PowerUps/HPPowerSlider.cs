using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPPowerSlider : MonoBehaviour
{
    Regenerate regenerate;
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
                regenerate = bm.gameObject.GetComponent<Regenerate>();
                Debug.Log(regenerate);
                return;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        mSlider.value = -regenerate.returnPowerUpCooling();
    }
}
