using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxInfo : MonoBehaviour {
	Camera main;
	GravityWarp mainWarp;
	public char gravity;
	// Use this for initialization
	void Start () {
		main = Camera.main;
		mainWarp = main.GetComponent<GravityWarp>();
	}
	
	// Update is called once per frame
	
		void OnCollisionEnter2D(Collision2D other)
	{
		if(other.transform.CompareTag("box destruct")){
			mainWarp.boxes.Remove(gameObject.transform);
			Destroy(gameObject);
			}
		else if(!(other.transform.CompareTag("Untagged")) && gameObject.CompareTag("destructable")){
			
			if(other.transform.GetComponent<Rigidbody2D>().velocity.x >8f){
				mainWarp.boxes.Remove(gameObject.transform);
				Destroy(gameObject);
			}
			if(other.transform.GetComponent<Rigidbody2D>().velocity.x < -8f){
				mainWarp.boxes.Remove(gameObject.transform);
				Destroy(gameObject);
			}	
			if(other.transform.GetComponent<Rigidbody2D>().velocity.y >8f){
				mainWarp.boxes.Remove(gameObject.transform);
				Destroy(gameObject);
			}
			if(other.transform.GetComponent<Rigidbody2D>().velocity.y < -8f){
				mainWarp.boxes.Remove(gameObject.transform);
				Destroy(gameObject);
			}
		}
	}
}


