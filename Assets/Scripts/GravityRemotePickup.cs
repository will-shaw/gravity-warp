using UnityEngine;

public class GravityRemotePickup : MonoBehaviour {
    bool once;

    //Calls disableAutoGravity when gravity remote is pickuped by player
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player" && !once){
            once = true;
            GetComponent<AudioSource>().Play();
            Camera.main.GetComponent<Level_1Control>().disableAutoGravity();
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        }
	}
}
