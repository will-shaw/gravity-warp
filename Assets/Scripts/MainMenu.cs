using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Transform field;
	public string scene;
    public UnityEngine.UI.Button button;

    void Start()
    {
		button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { field.GetComponent<Field>().ToggleField(); });
    }

	void Update() {
		if (gameObject.GetComponent<Button>().active) {
			SceneManager.LoadScene(scene);
		}
	}

}
