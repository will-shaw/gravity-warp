using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour {
	bool on = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		on = true;
		Debug.Log(true);
	}

	
	void OnTriggerExit2D(Collider2D other)
	{
		on = false;
		Debug.Log(false);
	}
}
