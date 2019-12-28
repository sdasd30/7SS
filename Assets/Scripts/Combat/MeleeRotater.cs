using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRotater : MonoBehaviour
{
    public bool rotated;
    public float degreesRotation;
    private Transform mPosition;
    // Start is called before the first frame update
    void Start()
    {
        mPosition = GetComponent<Transform>();
        mPosition.localRotation = Quaternion.Euler (new Vector3(0f, 0f, degreesRotation / 2f));
    }

    public void RotateMe()
    {
        if (rotated)
        {
            mPosition.localRotation = Quaternion.Euler(new Vector3(0f, 0f, degreesRotation / 2f));
            rotated = false;
        }

        else
        {
            mPosition.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -degreesRotation / 2f));
            rotated = true;
        }
    }
}
