using UnityEngine;

public class Spike : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			Camera.main.GetComponent<GravityWarp>().playerDead = true;
			Destroy (other.gameObject.GetComponent<Player>().canvas.gameObject);
			Destroy(other.gameObject);
		}
	}
}
