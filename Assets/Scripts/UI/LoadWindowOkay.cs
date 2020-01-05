using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Json;
//using System.Runtime.Serialization.Formatters.Binary;

public class LoadWindowOkay : MonoBehaviour
{
    public GameObject TextboxWindow;
    public InputField LoadTextField;
    public Text TitleMessage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Confirm()
    {
        string data = LoadTextField.text;
        //BinaryFormatter bf = new BinaryFormatter();
        //MemoryStream ms = new MemoryStream();
        //StreamWriter sw = new StreamWriter(ms);
        //sw.Write(data);
        //SaveObject oldSave = (SaveObject)bf.Deserialize(ms);
        //FindObjectOfType<StatTracker>().LoadFromSaveObject(oldSave);
        try
        {
            SaveObject so = JsonUtility.FromJson<SaveObject>(data);
            FindObjectOfType<StatTracker>().LoadFromSaveObject(so);
        }
        catch
        {
            TitleMessage.text = "Could not Load Save File!";
            return;
        }
        TitleMessage.text = "Load Successful!";
    }
    public void Close()
    {
        TextboxWindow.SetActive(false);
    }
}
