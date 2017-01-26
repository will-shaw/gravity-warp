using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour {

	bool glued;

	float gluedTime;

	public float glueTime;


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
			glued = true;
			gluedTime = glueTime;
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
			Camera.main.GetComponent<GravityWarpGlue>().changeGlueCount(0);
	}


	public bool isGlued (){
		return glued;
	}
}
