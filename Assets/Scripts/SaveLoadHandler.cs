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

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player-progress.dat", FileMode.Create);
        PlayerData data = new PlayerData();
        data.SetLevel(playerScene);
        data.SetTime(Info.gameTime);
        bf.Serialize(file, data);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player-progress.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player-progress.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            playerScene = data.GetLevel();
            Info.gameTime = data.GetTime();
        }
    }
}

[Serializable]
class PlayerData
{
    string level;

    float time;

    public void SetLevel(string l)
    {
        this.level = l;
    }

    public string GetLevel()
    {
        return this.level;
    }

    public void SetTime(float t)
    {
        this.time = t;
    }

    public float GetTime()
    {
        return this.time;
    }

}