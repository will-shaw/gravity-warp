using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadHandler : MonoBehaviour
{
    public static SaveLoadHandler master;
    public static string playerScene;
     void Awake()
    {
        // Basically makes this a singleton, in case we ever instantiate.
        if (master == null)
        {
            DontDestroyOnLoad(gameObject);
            master = this;
        }
        else if (master != this)
        {
            Destroy(gameObject);
        }
    }

    public static void Save(string savename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player-" + savename + ".dat", FileMode.Create);
        PlayerData data = new PlayerData();
        data.SetLevel(playerScene);
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load(string savename)
    {
        if (File.Exists(Application.persistentDataPath + "/player-" + savename + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player-" + savename + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            playerScene = data.GetLevel();
        }
    }
}

[Serializable]
class PlayerData
{
   
    string level;
   

  

    public void SetLevel(string l)
    {
        this.level = l;
    }
    public string GetLevel()
    {
        return this.level;
    }

}