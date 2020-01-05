using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveGame : MonoBehaviour
{
    StatTracker stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = FindObjectOfType<StatTracker>();
    }

    // Update is called once per frame
    public void WriteSaveToFile()
    {
        SaveObject newSave = stats.TransferToSaveObject();

        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath +"\\gamesave.sav"); //
        //Debug.Log("Saved Game to " + Application.persistentDataPath + "\\gamesave.sav");
        //bf.Serialize(file, newSave);
        //file.Close();

        string json = JsonUtility.ToJson(newSave);
        //StreamWriter sw = File.CreateText(Application.persistentDataPath + "\\gamesave.sav");
        //sw.Close();
        File.WriteAllText(Application.persistentDataPath + "\\gamesave.sav",json);
        Debug.Log("Game Saved");
    }
}
