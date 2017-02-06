using UnityEngine;
using UnityEngine.UI;
public class Pausehandler : MonoBehaviour {

    float timer =0f;
    bool active = false;
    public GameObject controls;

    public GameObject player;
    public void showControls()
    {
        
        gameObject.SetActive(false);
        controls.SetActive(true);
        
    }

    public void hide() {
        gameObject.SetActive(false);
        Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
        Camera.main.GetComponent<GravityWarp>().time = true;
        player.GetComponent<Player>().paused=false;
    }
	 void update(){
        float time = Camera.main.GetComponent<GravityWarp>().leveltmr;
        Debug.Log(gameObject.transform.GetChild(0).GetComponent<Text>().text);
        Debug.Log("hello");
        gameObject.transform.GetChild(0).GetComponent<Text>().text = "Current Time: "+ string.Format( "{0:N2}", time);
    }
}
