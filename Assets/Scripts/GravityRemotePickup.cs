using UnityEngine;

public class GravityRemotePickup : MonoBehaviour {

	//Calls disableAutoGravity when gravity remote is pickuped by player
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player"){
			Camera.main.GetComponent<Level_1Control>().disableAutoGravity();
			Destroy(gameObject);
		}
	}
}
