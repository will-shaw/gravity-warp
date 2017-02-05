using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform field; // The forcefield connected to this button.
    public string scene; // The scene to load when button activates.
    AsyncOperation op; // Allows level to load while box is falling, and activate with button.
    public UnityEngine.UI.Button button; // The UI button.

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { Load(); });
        if (name == "button_resume")
        {
            SaveLoadHandler.Load();
            if (SaveLoadHandler.playerScene != null)
            {
                scene = SaveLoadHandler.playerScene;
            }
        }
    }

    void Update()
    {
        if (GetComponent<Button>().active)
        {
            op.allowSceneActivation = true;
        }
    }

    void Load()
    {
        field.GetComponent<Field>().ToggleField();
        op = SceneManager.LoadSceneAsync(scene);
        op.allowSceneActivation = false;
    }

}
