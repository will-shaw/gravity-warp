using UnityEngine;

public class GravityRemotePickup : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player"){
			Camera.main.GetComponent<Level_1Control>().disableAutoGravity();
			Destroy(gameObject);
		}
	}
}
