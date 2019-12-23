using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public float scoreValue;
    private void OnDestroy()
    {
        FindObjectOfType<PlayerScore>().GetComponent<PlayerScore>().addScore(scoreValue);
    }
}
