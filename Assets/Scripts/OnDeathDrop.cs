using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDropItem
{
    public GameObject Item;
    public Vector2 Offset;
    public Vector2 XOffsetRange;
    public Vector2 YOffsetRange;
    public float Probability = 100f;
    public bool UseExcludeCategory = false;
    public int ExcludeCategory = 1;
    public bool InheritFaction = true;
}
public class OnDeathDrop : MonoBehaviour
{
    public List<DeathDropItem> DeathItems = new List<DeathDropItem>();

    private List<DeathDropItem> no_exclude_items = new List<DeathDropItem>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        foreach (DeathDropItem item in no_exclude_items)
        {
            if (Random.Range(0f,100f) < item.Probability)
            {
                spawnItem(item);
            }      
        }
    }
    private void spawnItem(DeathDropItem item)
    {
        Vector3 pos = new Vector3(transform.position.x + Random.Range(item.XOffsetRange.x, item.XOffsetRange.y),
                    transform.position.y + Random.Range(item.YOffsetRange.x, item.YOffsetRange.y), transform.position.z);
        Instantiate(item.Item, pos, Quaternion.identity);
        if (item.InheritFaction)
        {
        }
    }
}
