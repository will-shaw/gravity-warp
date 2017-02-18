using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform field; // The forcefield connected to this button.
    public string scene; // The scene to load when button activates.
    AsyncOperation op; // Allows level to load while box is falling, and activate with button.
    public UnityEngine.UI.Button button; // The UI button.
    public Rigidbody2D warpText; // Only set in ONE MainMenu script.

    public UnityEngine.UI.Button[] otherButtons;

    public float warpTextDelay;

    float warpTextTimer;

    void Start()
    {
        warpTextTimer = warpTextDelay;
        GravityWarp.gravity = "D";
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { Load(); });
        if (name == "button_resume")
        {
            SaveLoadHandler.Load();
            if (SaveLoadHandler.playerScene != null && SaveLoadHandler.playerScene != "credits")
            {
                scene = SaveLoadHandler.playerScene;
            }
            else
            {
                scene = "Story";
                Info.gameTime = 0;
            }
        }
    }

    void Update()
    {
        if (warpText != null)
        {
            warpTextTimer -= Time.deltaTime;
            if (warpTextTimer <= 0)
            {
                warpText.gravityScale = -4;
                warpTextTimer = warpTextDelay;
            }
        }

        if (GetComponent<Button>().active)
        {
            if (name == "button_left")
            {
                Info.gameTime = 0;
            }
            op.allowSceneActivation = true;
        }
    }

    void Load()
    {
        foreach (UnityEngine.UI.Button button in otherButtons)
        {
            button.enabled = false;
        }

        field.GetComponent<Field>().ToggleField();
        op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
    }

}
