using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    StatTracker stats;
    void Start()
    {
        stats = FindObjectOfType<StatTracker>();
        LoadSaveFromFile();
    }

    // Update is called once per frame
    public void LoadSaveFromFile()
    {
        if (File.Exists(Application.persistentDataPath + "\\gamesave.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "\\gamesave.sav", FileMode.Open);
            SaveObject oldSave = (SaveObject)bf.Deserialize(file);
            stats.LoadFromSaveObject(oldSave);
            file.Close();
            Debug.Log("Loaded game from " + Application.persistentDataPath + "\\gamesave.sav");
            return;
        }

        else
        {
            Debug.LogError("Load Failed! Maybe you need to create a savegame first?");
        }
    }
}
