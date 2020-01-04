using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWindowConfirm : MonoBehaviour
{
    public GameObject TextboxWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Confirm()
    {
        TextboxWindow.SetActive(false);
    }
}
