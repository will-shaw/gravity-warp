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

    bool isControlShown = false;
    bool isListening = false;
    string opt;
    string key;

    public static bool hasGravity = false;
    public static bool hasGlue = false;

    static private KeyCode[] validKeyCodes;

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

    public void SaveControls()
    {
        setcontrols.SetActive(false);
        isControlShown = false;
        UnityEngine.UI.Button[] buttons = pause.GetComponentsInChildren<UnityEngine.UI.Button>();
        for (int i = 0; i < buttons.Length - 1; i++)
        {
            buttons[i].enabled = true;
        }
        pause.transform.GetChild(5).GetComponentInChildren<UnityEngine.UI.Text>().text = "Controls";
        controls.SetActive(false);
        InputManager.Save();
    }

    public void ShowControls()
    {
        isControlShown = !isControlShown;
        if (isControlShown)
        {
            UnityEngine.UI.Button[] buttons = pause.GetComponentsInChildren<UnityEngine.UI.Button>();
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                buttons[i].enabled = false;
            }
            pause.transform.GetChild(5).GetComponentInChildren<UnityEngine.UI.Text>().text = "Close";
            controls.SetActive(isControlShown);
            LimitControlMenu(controls);
        }
        else
        {
            UnityEngine.UI.Button[] buttons = pause.GetComponentsInChildren<UnityEngine.UI.Button>();
            for (int i = 0; i < buttons.Length - 1; i++)
            {
                buttons[i].enabled = true;
            }
            pause.transform.GetChild(5).GetComponentInChildren<UnityEngine.UI.Text>().text = "Controls";
            controls.SetActive(isControlShown);
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
        panel.SetActive(false);
        death.SetActive(false);
        pause.SetActive(false);
        controls.SetActive(false);
        confirm.SetActive(false);
        setcontrols.SetActive(false);
        player.GetComponent<Player>().GetCanvas().gameObject.SetActive(true);
        Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
        Camera.main.GetComponent<GravityWarp>().time = true;
        player.GetComponent<Player>().paused = false;
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
                    setcontrols.transform.FindChild(key).GetComponentInChildren<UnityEngine.UI.Text>().text = val.ToString();
                    Debug.Log("Success: " + key + " set to " + val);
                    this.key = null;
                    isListening = false;
                    UnityEngine.UI.Button[] buttons = setcontrols.GetComponentsInChildren<UnityEngine.UI.Button>();
                    foreach (UnityEngine.UI.Button button in buttons)
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
        if (Camera.main.GetComponent<GravityWarp>().playerDead)
        {
            Info.load = true;
        }
        else
        {
            Info.load = false;
        }
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        GravityWarp.gravity = "D";
    }

    public void MainMenu()
    {
        Hide();
        SceneManager.LoadScene("main_menu");
    }

    public void LimitControlMenu(GameObject controls){
        if(!(hasGravity)) {
            controls.transform.GetChild(3).gameObject.SetActive(false);
            controls.transform.GetChild(9).gameObject.SetActive(false);
            controls.transform.GetChild(10).gameObject.SetActive(false);
        }
        if(!(hasGlue)) {
            controls.transform.GetChild(1).gameObject.SetActive(false);
            controls.transform.GetChild(11).gameObject.SetActive(false);
        }
    }
}
