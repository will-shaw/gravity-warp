using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour {

	bool glued;

	float gluedTime;

	public float glueTime;

	public Transform glue;

	// Use this for initialization
	void Start () {
		glued  = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(glued && gluedTime > 0){
			gluedTime -= Time.deltaTime;
		} else if (glued) {
			glued = false;
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
		}
		
	}


	public void gluing() {
		if(glued){
				glued = false;
				gluedTime = 0;
				GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
			} else {
				glued = true;
				gluedTime = glueTime;
				GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			}
	}


	public bool isGlued (){
		return glued;
	}
}
