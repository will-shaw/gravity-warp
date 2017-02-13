using UnityEngine;

public class Spike : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			if(Camera.main.GetComponent<GravityWarp>().changetmr >0.3f){
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			//Destroy (other.gameObject.GetComponent<Player>().canvas.gameObject);
			//Destroy(other.gameObject);
				GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetSpikeKill(), 1);
			}                    
		}
	}
}
