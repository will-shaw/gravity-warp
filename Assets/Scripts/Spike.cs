using UnityEngine;

public class Spike : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			Destroy(other.gameObject);
		}
	}
}
