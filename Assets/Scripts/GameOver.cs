using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{


    AsyncOperation op; // Holds the async operation. Used to 'allowSceneActivation'.

    void Start()
    {
        op = SceneManager.LoadSceneAsync("facility_intro");
        op.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadIntro()
    {
        op.allowSceneActivation = true;
    }

}
