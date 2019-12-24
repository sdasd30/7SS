using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float scoreValue = 1;
    public float value = 1;
    private void OnDestroy()
    {
        FindObjectOfType<PlayerScore>().GetComponent<PlayerScore>().addScore(scoreValue);
    }
}
