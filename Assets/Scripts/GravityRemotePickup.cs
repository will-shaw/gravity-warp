using UnityEngine;

public class GravityRemotePickup : MonoBehaviour {
    bool once;
    public GameObject checkpoint;

    public GameObject menu;


    public float timer;
    //Calls disableAutoGravity when gravity remote is pickuped by player
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && !once){
            once = true;
            Info.checkpoint = new Vector3(-11.6f,7f,0f);
            Info.checktime = Camera.main.GetComponent<GravityWarp>().leveltmr;
            GetComponent<AudioSource>().Play();
            Camera.main.GetComponent<Level_1Control>().disableAutoGravity();
            Camera.main.GetComponent<GravityWarp>().checktmr = 2f;
            checkpoint.SetActive(true);
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            MenuHandler.hasGravity = true;
            if(Info.GravInfo)
            {
                menu.GetComponent<MenuHandler>().ShowGravityControls();
                Info.GravInfo = false;
            }
        }
	}

}
