using UnityEngine;

public class PauseHandler : MonoBehaviour {

    float timer =0f;
    bool active = false;
    public GameObject controls;

    public void showControls()
    {
        controls.SetActive(true);
        gameObject.SetActive(false);
    }

    public void hide() {
        gameObject.SetActive(false);
    }
	
}
