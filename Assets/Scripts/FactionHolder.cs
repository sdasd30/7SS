using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType { IMMUNE, ALLIES, ENEMIES, NEUTRAL };


public class FactionHolder : MonoBehaviour
{
    public FactionType Faction = FactionType.NEUTRAL;
    public bool CanAttack(FactionType otherFaction)
    {
        return (otherFaction == FactionType.NEUTRAL || Faction == FactionType.NEUTRAL || otherFaction != Faction);
    }
    public bool CanAttack(FactionHolder f)
    {
        return CanAttack(f.Faction);
    }
    public bool CanAttack(Attackable otherObj)
    {
        Debug.Log(otherObj);
        return CanAttack(otherObj.GetComponent<FactionHolder>().Faction);
    }
    public bool CanAttack(GameObject go)
    {
        if (go.GetComponent<FactionHolder>())
            return CanAttack(go.GetComponent<FactionHolder>().Faction);
        return true;
    }
    public void SetFaction(GameObject go)
    {
        if (go.GetComponent<FactionHolder>() == null)
            go.AddComponent<FactionHolder>();
        go.GetComponent<FactionHolder>().Faction = Faction;
    }
}
