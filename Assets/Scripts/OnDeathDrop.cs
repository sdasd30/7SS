using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    private void Start()
    {
        foreach ( DeathDropItem item in DeathItems)
        {
            if (!item.UseExcludeCategory)
                no_exclude_items.Add(item);
        }
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
        GameObject go = Instantiate(item.Item, pos, Quaternion.identity);
        if (item.InheritFaction && GetComponent<FactionHolder>() != null)
        {
            GetComponent<FactionHolder>().SetFaction(go);
        }
    }
}
