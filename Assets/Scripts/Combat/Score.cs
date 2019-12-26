using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public string TrackName;
    public float scoreValue = 1; //Points scored
    public float value = 1; //Spawn cost
    private void OnDestroy()
    {
        FindObjectOfType<PlayerScore>().addScore(scoreValue);
    }
}
