using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InputManager : MonoBehaviour
{

    /* Sets Control Scheme */
    public static int controlScheme = 0;

    /* Left Hand. */
    public static KeyCode axisUp = KeyCode.W;
    public static KeyCode axisDown = KeyCode.S;
    public static KeyCode axisLeft = KeyCode.A;
    public static KeyCode axisRight = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    public static KeyCode zoom = KeyCode.Tab;

    /* Right Hand. */
    public static KeyCode gravityUp = KeyCode.Mouse0;
    public static KeyCode gravityDown = KeyCode.Mouse0;
    public static KeyCode gravityLeft = KeyCode.Mouse0;
    public static KeyCode gravityRight = KeyCode.Mouse0;
    public static KeyCode glue = KeyCode.Mouse1;

    /* Menus */
    public static KeyCode menu = KeyCode.Escape;

    void Start()
    {
        UpdateControls();
        Load();
    }

    void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player-settings.dat", FileMode.Create);
        ControlSettings data = new ControlSettings();
        data.axisUp = axisUp;
        data.axisDown = axisDown;
        data.axisLeft = axisLeft;
        data.axisRight = axisRight;

        data.jump = jump;
        data.zoom = zoom;
        data.gravityUp = gravityUp;
        data.gravityDown = gravityDown;
        data.gravityLeft = gravityLeft;
        data.gravityRight = gravityRight;
        data.glue = glue;

        bf.Serialize(file, data);
        file.Close();
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player-settings.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player-settings.dat", FileMode.Open);
            ControlSettings data = (ControlSettings)bf.Deserialize(file);
            file.Close();
            axisUp = data.axisUp;
            axisDown = data.axisDown;
            axisLeft = data.axisLeft;
            axisRight = data.axisRight;

            jump = data.jump;
            zoom = data.zoom;
            gravityUp = data.gravityUp;
            gravityDown = data.gravityDown;
            gravityLeft = data.gravityLeft;
            gravityRight = data.gravityRight;
            glue = data.glue;
        }
    }

    void UpdateControls()
    {
        switch (controlScheme)
        {
            case 0:
                gravityUp = KeyCode.Mouse0;
                gravityDown = KeyCode.Mouse0;
                gravityLeft = KeyCode.Mouse0;
                gravityRight = KeyCode.Mouse0;
                glue = KeyCode.Mouse1;
                break;
            case 1:
                gravityUp = KeyCode.UpArrow;
                gravityDown = KeyCode.DownArrow;
                gravityLeft = KeyCode.LeftArrow;
                gravityRight = KeyCode.RightArrow;
                glue = KeyCode.LeftControl;
                break;
        }
    }

}

[Serializable]
class ControlSettings
{
    public KeyCode axisUp;
    public KeyCode axisDown;
    public KeyCode axisLeft;
    public KeyCode axisRight;
    public KeyCode jump;
    public KeyCode zoom;
    public KeyCode glue;
    public KeyCode gravityUp;
    public KeyCode gravityDown;
    public KeyCode gravityLeft;
    public KeyCode gravityRight;
}