using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObject
{
    public List<string> savAchivementsDone;
    public Dictionary<string, int> savMaxEnemykills;
    public Dictionary<string, int> savMaxWeaponKills;
    public Dictionary<string, int> savMaxWeaponScores;

    public Dictionary<string, int> savLifetimeEnemykills;
    public Dictionary<string, int> savLifetimeWeaponKills;
    public Dictionary<string, int> savLifetimeWeaponScores;

    public Dictionary<string, int> savMaxWeaponSwitches;
    public Dictionary<string, int> savLifetimeWeaponSwitches;

    public Dictionary<string, int> savMaxWeaponUsedAtLevel;

    public int savmaxScore;
    public int savmaxKills;
    public int savmaxSwitches;
}
