using UnityEngine;

public class Pausehandler : MonoBehaviour {

    float timer =0f;
    bool active = false;
    public GameObject controls;

    public void showControls()
    {
        
        gameObject.SetActive(false);
        controls.SetActive(true);
        
    }

    public void hide() {
        gameObject.SetActive(false);
        Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
    }
	
}
