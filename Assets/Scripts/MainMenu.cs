using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Transform field;
    public string scene;
    AsyncOperation op;
    public UnityEngine.UI.Button button;

    void Start()
    {
        button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { Load(); });
    }

    void Update()
    {
        if (gameObject.GetComponent<Button>().active)
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
