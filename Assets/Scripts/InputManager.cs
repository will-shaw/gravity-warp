using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InputManager : MonoBehaviour
{
    public static int gravityControlScheme = 0;
    public static int glueControlScheme = 0;
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
    public static KeyCode glue2 = KeyCode.P;

    /* Menus */
    public static KeyCode menu = KeyCode.Escape;

    void Start()
    {
        Load();
    }

    public static bool Set(string key, KeyCode val)
    {
        switch (key)
        {
            case "axisUp":
                axisUp = val;
                return true;
            case "axisDown":
                axisDown = val;
                return true;
            case "axisLeft":
                axisLeft = val;
                return true;
            case "axisRight":
                axisRight = val;
                return true;
            case "jump":
                jump = val;
                return true;
            case "zoom":
                zoom = val;
                return true;
            case "glue":
                glue = val;
                return true;
            case "gravityUp":
                gravityUp = val;
                return true;
            case "gravityDown":
                gravityDown = val;
                return true;
            case "gravityLeft":
                gravityLeft = val;
                return true;
            case "gravityRight":
                gravityRight = val;
                return true;
        }
        return false;
    }

    public static KeyCode GrabKey(string key)
    {
        switch (key)
        {
            case "axisUp":
                return axisUp;
            case "axisDown":
                return axisDown;
            case "axisLeft":
                return axisLeft;
            case "axisRight":
                return axisRight;
            case "jump":
                return jump;
            case "zoom":
                return zoom;
            case "glue":
                return glue;
            case "gravityUp":
                return gravityUp;
            case "gravityDown":
                return gravityDown;
            case "gravityLeft":
                return gravityLeft;
            case "gravityRight":
                return gravityRight;
            case "menu":
                return menu;
        }
        return KeyCode.None;
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player-controls.dat", FileMode.Create);
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

        if (gravityUp.ToString().Substring(0, 5) == "Mouse")
        {
            Debug.Log("Gravity using mouse control");
            gravityControlScheme = 0;
        }
        else
        {
            gravityControlScheme = 1;
        }
        if (glue.ToString().Substring(0, 5) == "Mouse")
        {
            Debug.Log("Glue using mouse control");
            glueControlScheme = 0;
        }
        else
        {
            glueControlScheme = 1;
        }

        Debug.Log("Controls saved.");
    }

    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/player-controls.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player-controls.dat", FileMode.Open);
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

            if (gravityUp.ToString().Substring(0, 5) == "Mouse")
            {
                Debug.Log("Gravity using mouse control");
                gravityControlScheme = 0;
            }
            else
            {
                gravityControlScheme = 1;
            }
            if (glue.ToString().Substring(0, 5) == "Mouse")
            {
                Debug.Log("Glue using mouse control");
                glueControlScheme = 0;
            }
            else
            {
                glueControlScheme = 1;
            }

            glue = data.glue;
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