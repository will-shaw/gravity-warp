using UnityEngine;
using UnityEngine.UI;

public class JSConnector : MonoBehaviour
{

    public GameObject nameInput;
    public GameObject btnSubmit;
    public Text nameField;
    public Text time;

    void Start() {
        time.text = "Your time:\n" + Info.gameTime;
    }

    public void PostScore()
    {
        if (nameField.text.Length >= 4)
        {
            Application.ExternalCall("PostScore", nameField.text, Info.gameTime);
            btnSubmit.SetActive(false);
            nameInput.SetActive(false);
        } else {
            nameInput.transform.FindChild("Placeholder").GetComponent<Text>().text = "Name too short";
        }
    }

    public void LoadScoreboard()
    {
        Application.ExternalCall("LoadScoreboard");
    }

}
