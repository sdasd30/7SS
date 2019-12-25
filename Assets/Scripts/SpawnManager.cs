using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    DependentSpawner[] spawners;
    public List<GameObject> enemies;
    public GameObject nextEnemy;
    private DependentSpawner nextSpawner;
    CreditPool creditPool;


    void Start()
    {
        spawners = FindObjectsOfType<DependentSpawner>();
        creditPool = GetComponent<CreditPool>();
        nextSpawner = searchSpawner();
        nextEnemy = decideEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if (creditPool.spendCredit(nextEnemy.GetComponent<Score>().value))
        {
            nextSpawner.spawn(nextEnemy);
            nextSpawner = searchSpawner();
            nextEnemy = decideEnemy();
        }
    }

    private DependentSpawner searchSpawner()
    {
        return spawners[Random.Range(0, spawners.Length)];
    }


    private GameObject decideEnemy()
    {
        GameObject considering;
        considering = enemies[Random.Range(0, enemies.Count)];
        //Debug.Log("initial enemy is " + considering);
        Debug.Log(creditPool.incrementby * 3);
        while (considering.GetComponent<Score>().value > (creditPool.incrementby * 3))
        {
            //Debug.Log("searching for a new enemy...");
            considering = enemies[Random.Range(0, enemies.Count)];
        }
        //private GameObject considering;
        //while ()
        //if !(enemies[Random.Range(0, enemies.Count)])
        //Debug.Log("seems reasonable, returning " + considering);
        return considering;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, .8f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
