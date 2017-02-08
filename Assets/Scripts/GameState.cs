using UnityEngine;

public class GameState : MonoBehaviour
{

    static bool isPaused = false;
    static bool hasGun = false;
    static bool hasRemote = false;
    static bool glueEnabled = false;
    static bool gravityEnabled = false;

    public static bool GlueEnabled()
    {
        return glueEnabled;
    }

    public static void GlueEnabled(bool b)
    {
        glueEnabled = b;
    }

    public static bool GravityEnabled()
    {
        return gravityEnabled;
    }

    public static void GravityEnabled(bool b)
    {
        gravityEnabled = b;
    }

    public static bool HasGun()
    {
        return hasGun;
    }

    public static void HasGun(bool b)
    {
        hasGun = b;
    }

    public static bool HasRemote()
    {
        return hasRemote;
    }

    public static void HasRemote(bool b)
    {
        hasRemote = b;
    }

    public static void Pause(bool b)
    {
        isPaused = b;
    }

    public static bool IsPaused()
    {
        return isPaused;
    }


}
