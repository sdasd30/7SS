using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    private Dictionary<FactionType, List<FactionHolder>> allFactionedObjects = new Dictionary<FactionType, List<FactionHolder>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterAttackable(FactionHolder go)
    {
        FactionType ft = go.Faction;
        if (!allFactionedObjects.ContainsKey(ft))
        {
            allFactionedObjects[ft] = new List<FactionHolder>();
        }
            
        allFactionedObjects[ft].Add(go);
    }

    public void DeregisterAttackable(FactionHolder go)
    {
        FactionType ft = go.Faction;
        if (!allFactionedObjects.ContainsKey(ft) || !allFactionedObjects[ft].Contains(go))
            return;
        allFactionedObjects[ft].Remove(go);
    }

    public FactionHolder GetClosestOpposingFactionObj(FactionHolder go)
    {
        Vector3 pos = go.gameObject.transform.position;
        float minDistance = float.MaxValue;
        FactionHolder closest = null;
        foreach(FactionType enemy in go.GetOpposingFactions())
        {
            if (!allFactionedObjects.ContainsKey(enemy))
                continue;
            foreach (FactionHolder fh in allFactionedObjects[enemy])
            {
                float d = Vector3.Distance(pos, fh.gameObject.transform.position);
                if (d < minDistance)
                {

                    minDistance = d;
                    closest = fh;
                }
            }
        }
        if (go.Faction == FactionType.ALLIES)
        {
            //Debug.Log("New Closest: " + pos + " : " + closest.transform.position + " d: " + d + " o: " + fh.gameObject);

        }
        return closest;
    }
}
