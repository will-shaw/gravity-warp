
using UnityEngine;

public class BoxColision : MonoBehaviour {
	Camera main;
	GravityWarp mainWarp;

    public bool metalBox;

	public AudioClip[] metalClips = new AudioClip[3];
    public AudioClip[] woodClips = new AudioClip[3];

    float velocityY =0f;
	float velocityX=0f;
	float velocityY1 =0f;
	float velocityX1=0f;
	// Use this for initialization
	void Start () {
		main = Camera.main;
		mainWarp = main.GetComponent<GravityWarp>();
	}
	
	// Update is called once per frame
	void Update(){
		if(velocityX< gameObject.transform.GetComponent<Rigidbody2D>().velocity.x ){
			velocityX = gameObject.transform.GetComponent<Rigidbody2D>().velocity.x;
		}
		if(velocityY< gameObject.transform.GetComponent<Rigidbody2D>().velocity.y ){
			velocityY = gameObject.transform.GetComponent<Rigidbody2D>().velocity.y;
		}
		if(velocityX1> gameObject.transform.GetComponent<Rigidbody2D>().velocity.x ){
			velocityX1 = gameObject.transform.GetComponent<Rigidbody2D>().velocity.x;
		}
		if(velocityY1> gameObject.transform.GetComponent<Rigidbody2D>().velocity.y ){
			velocityY1 = gameObject.transform.GetComponent<Rigidbody2D>().velocity.y;
		}
		
	}
		void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Wall") {
            AudioClip hit = null;
            if (metalBox) {
                hit = metalClips[Random.Range(0, 3)];
            } else {
                hit = woodClips[Random.Range(0, 3)];
            }
            GetComponent<AudioSource>().PlayOneShot(hit, 1);
        }
		
		if(other.transform.CompareTag("box destruct")){
			mainWarp.boxes.Remove(gameObject.transform);
			Destroy(gameObject);
			}
		else if(!(other.transform.CompareTag("Wall"))&& !(other.transform.CompareTag("Player"))){
			if(other.gameObject.tag == "destructable") {

				if(velocityX1 >30f){
					mainWarp.boxes.Remove(other.transform);
					Destroy(other.gameObject);
				}
				if(velocityX< -30f){
					mainWarp.boxes.Remove(other.transform);
					Destroy(other.gameObject);
				}	
				if(velocityY >30f){
					mainWarp.boxes.Remove(other.transform);
					Destroy(other.gameObject);
				}
				if(velocityY1< -30f){
					mainWarp.boxes.Remove(other.transform);
					Destroy(other.gameObject);
				}
			}
		}
		else if((other.transform.CompareTag("Player"))){
			if(velocityY >20f){
				Destroy(other.gameObject);
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			}
			if(velocityX >20f){
				Destroy(other.gameObject);
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			}
			if(velocityY1 < -20f){
				Destroy(other.gameObject);
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			}
			if(velocityX1 < -20f){
				Destroy(other.gameObject);
				Camera.main.GetComponent<GravityWarp>().playerDead = true;
			}
		}
		velocityX =0f;
		velocityY =0f;
		velocityX1 =0f;
		velocityY1 =0f;
	}
}