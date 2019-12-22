using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoxes : MonoBehaviour
{
    public GameObject Player;
    public GameObject HPBox;
    Attackable playerAtackable;
    int maxHP;
    // Start is called before the first frame update
    void Start()
    {
        playerAtackable = Player.GetComponent<Attackable>();
    }

    // Update is called once per frame
    void Update()
    {
        maxHP = (int) playerAtackable.maxHP;
        if (transform.childCount < maxHP)
        {
            GameObject box = Instantiate(HPBox, new Vector3(), new Quaternion());
            box.transform.parent = this.transform;
        }

        if (transform.childCount > maxHP)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
