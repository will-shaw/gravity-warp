using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Emit : MonoBehaviour {
	bool activ = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void toggle(){
		if(!activ){
			gameObject.GetComponent<ParticleSystem>().Play();
			activ = true;
		}else{
			gameObject.GetComponent<ParticleSystem>().Stop();
			activ = false;
		}

	}
}
