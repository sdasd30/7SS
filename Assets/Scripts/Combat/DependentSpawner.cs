using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependentSpawner : MonoBehaviour
{
    public void spawn(GameObject x)
    {
        Instantiate(x, this.transform.position, Quaternion.identity );
    }
}
