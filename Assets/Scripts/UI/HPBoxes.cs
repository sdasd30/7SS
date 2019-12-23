using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBoxes : MonoBehaviour
{
    public GameObject player;
    public GameObject HPBox;
    Attackable playerAtackable;
    int maxHP;
    // Start is called before the first frame update
    void Start()
    {
        playerAtackable = player.GetComponent<Attackable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) updateBoxes();
    }

    private void updateBoxes()
    {
        maxHP = (int)playerAtackable.maxHP;
        if (transform.childCount < maxHP)
        {
            GameObject box = Instantiate(HPBox, new Vector3(), new Quaternion());
            box.transform.SetParent(this.transform);
        }

        if (transform.childCount > maxHP)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
