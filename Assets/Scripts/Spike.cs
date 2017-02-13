using UnityEngine;

public class Spike : MonoBehaviour {
	bool contact= false;
	float tmr= 0.0f;
	//Transform player;

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			//player = other.transform;
			//if(Camera.main.GetComponent<GravityWarp>().changetmr >0.1f){
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			//Destroy (other.gameObject.GetComponent<Player>().canvas.gameObject);
			//Destroy(other.gameObject);
				GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetSpikeKill(), 1);
			//}
			contact = true;                    
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{	
		tmr =0;
		contact =false;
	}
	
	void Update()
	{
		if(contact){
			tmr += Time.deltaTime;
			if(tmr >0.2f){
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			}
		}	
	}
}
