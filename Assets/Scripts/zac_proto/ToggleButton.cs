using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {
	bool on =false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	void OnTriggerEnter2D()
	{
		if(on){
			on = false;
			Debug.Log(false);
		}else{
			on = true;
			Debug.Log(true);
		}
	}
}
