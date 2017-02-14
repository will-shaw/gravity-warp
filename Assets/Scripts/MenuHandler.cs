using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Transform player;

    GameObject panel;
    GameObject death;
    GameObject pause;
    GameObject controls;
    GameObject confirm;
    GameObject setcontrols;
    GameObject gravityControls;
    bool gravityControlsShown = false;
    GameObject glueControls;
    GameObject playerDetails;
    bool glueControlsShown = false;

    bool isControlShown = false;
    bool isListening = false;
    bool isOptions = false;
    string opt;
    string key;

    public AudioClip successClip;

    public static bool hasGravity = false;
    public static bool hasGlue = false;

    static private KeyCode[] validKeyCodes;

    UnityEngine.UI.Button[] buttons;

    void Awake()
    {
        if (validKeyCodes != null) return;
        validKeyCodes = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
    }

    void Start()
    {
        panel = gameObject.transform.FindChild("Panel").gameObject;
        death = gameObject.transform.FindChild("DeathPanel").gameObject;
        pause = gameObject.transform.FindChild("PausePanel").gameObject;
        controls = gameObject.transform.FindChild("Controls").gameObject;
        confirm = gameObject.transform.FindChild("Confirm").gameObject;
        setcontrols = gameObject.transform.FindChild("SetControls").gameObject;
        gravityControls = gameObject.transform.FindChild("GravityInstructionsPanel").gameObject;
        glueControls = gameObject.transform.FindChild("GlueInstructionsPanel").gameObject;
        playerDetails = gameObject.transform.FindChild("PlayerDetails").gameObject;

        buttons = pause.GetComponentsInChildren<UnityEngine.UI.Button>();
        pause.transform.FindChild("btnEdit").gameObject.SetActive(false);

        pause.transform.FindChild("MusicVolume").GetComponent<UnityEngine.UI.Slider>().value = MusicPlayer.Instance.GetMusicVolume();
    }

    public void ShowPause()
    {
        // Pause game.
        panel.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(true);
    }

    public void ShowDeath()
    {
        // pause game.
        panel.SetActive(true);
        death.SetActive(true);
    }

    public void ShowGravityControls()
    {
        if (gravityControlsShown)
        {
            gravityControls.SetActive(false);
            gravityControlsShown = false;
        }
        else
        {
            gravityControls.SetActive(true);
            gravityControlsShown = true;
        }
    }

    public void ShowGlueControls()
    {
        if (glueControlsShown)
        {
            glueControls.SetActive(false);
            glueControlsShown = false;
        }
        else
        {
            glueControls.SetActive(true);
            glueControlsShown = true;
        }
    }

    public void SaveControls()
    {
        setcontrols.SetActive(false);
        isControlShown = false;
        for (int i = 0; i < buttons.Length - 1; i++)
            if (!isOptions)
            {
                if (buttons[i].GetComponentInChildren<UnityEngine.UI.Text>().text == "Edit")
                {
                    buttons[i].gameObject.SetActive(false);
                }
                else
                {
                    buttons[i].gameObject.SetActive(true);
                }
            }
            else
            {
                if (i == 0)
                {
                    buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }

            }
        pause.transform.FindChild("btnControls").GetComponentInChildren<UnityEngine.UI.Text>().text = "Controls";
        controls.SetActive(false);
        InputManager.Save();
    }

    public void ShowControls()
    {
        isControlShown = !isControlShown;
        if (isControlShown)
        {
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                if (buttons[i].GetComponentInChildren<UnityEngine.UI.Text>().text == "Edit")
                {
                    buttons[i].gameObject.SetActive(true);
                }
                else
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }
            pause.transform.FindChild("btnControls").GetComponentInChildren<UnityEngine.UI.Text>().text = "Close";
            controls.SetActive(isControlShown);
            LimitControlMenu(controls);
        }
        else
        {
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                if (!isOptions)
                {
                    if (buttons[i].GetComponentInChildren<UnityEngine.UI.Text>().text == "Edit")
                    {
                        buttons[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        buttons[i].gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        buttons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        buttons[i].gameObject.SetActive(false);
                    }
                }
            }
            pause.transform.FindChild("btnControls").GetComponentInChildren<UnityEngine.UI.Text>().text = "Controls";
            controls.SetActive(isControlShown);
        }
    }

    public void ShowOptions()
    {
        isOptions = true;
        setcontrols.SetActive(false);
        panel.SetActive(true);
        controls.SetActive(false);
        pause.SetActive(true);
        for (int i = 1; i < buttons.Length - 1; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void Prompt(string opt)
    {
        this.opt = opt;
        confirm.SetActive(true);
    }

    public void Confirm(bool b)
    {
        if (b)
        {
            if (opt == "menu")
            {
                confirm.SetActive(false);
                MainMenu();
            }
            else if (opt == "reset")
            {
                confirm.SetActive(false);
                ResetLevel();
            }
        }
        else
        {
            confirm.SetActive(false);
        }
    }

    public void Hide()
    {
        if (isOptions)
        {
            isOptions = false;
        }
        panel.SetActive(false);
        playerDetails.SetActive(false);
        death.SetActive(false);
        pause.SetActive(false);
        controls.SetActive(false);
        confirm.SetActive(false);
        setcontrols.SetActive(false);
        if (player)
        {
            player.GetComponent<Player>().GetCanvas().gameObject.SetActive(true);
            player.GetComponent<Player>().paused = false;
            Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
            Camera.main.GetComponent<GravityWarp>().time = true;
        }
    }

    public void SetKey(string key)
    {
        this.key = key;
        isListening = true;
        UnityEngine.UI.Button[] buttons = setcontrols.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (UnityEngine.UI.Button button in buttons)
        {
            if (button.name != key)
            {
                button.enabled = false;
            }
        }
    }

    public void Update()
    {
        if (isListening)
        {
            KeyCode val = FetchKey();
            if (val != KeyCode.None)
            {
                bool success = InputManager.Set(key, val);
                if (success)
                {
                    GetComponent<AudioSource>().PlayOneShot(successClip, 1);
                    setcontrols.transform.FindChild(key).GetComponentInChildren<UnityEngine.UI.Text>().text = val.ToString();
                    Debug.Log("Success: " + key + " set to " + val);
                    this.key = null;
                    isListening = false;
                    UnityEngine.UI.Button[] btns = setcontrols.GetComponentsInChildren<UnityEngine.UI.Button>();
                    foreach (UnityEngine.UI.Button button in btns)
                    {
                        button.enabled = true;
                    }
                }
            }
        }
    }

    public void EditControls()
    {
        setcontrols.SetActive(true);
    }

    KeyCode FetchKey()
    {
        foreach (KeyCode key in validKeyCodes)
        {
            if (Input.GetKey(key) && (key != KeyCode.Escape))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    public void ResetLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        GravityWarp.gravity = "D";
    }

    public void checkpointLoad()
    {
        Info.load = true;
        ResetLevel();
    }
    public void MainMenu()
    {
        Hide();
        GravityWarp.gravity = "D";
        SceneManager.LoadScene("main_menu");
    }

    public void LimitControlMenu(GameObject controls)
    {
        if (!(hasGravity))
        {
            controls.transform.GetChild(2).gameObject.SetActive(false);
            controls.transform.GetChild(8).gameObject.SetActive(false);
            controls.transform.GetChild(9).gameObject.SetActive(false);
        }
        if (!(hasGlue))
        {
            controls.transform.GetChild(0).gameObject.SetActive(false);
            controls.transform.GetChild(10).gameObject.SetActive(false);
        }
    }
}
