using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {
	bool on =false;

	public Transform[] buttonItems;
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(on){
			on = false;
			Debug.Log(false);
			toggleItemStatus();
		}else{
			on = true;
			Debug.Log(true);
			toggleItemStatus();
		}
	}

	void toggleItemStatus() {
		foreach(Transform item in buttonItems) {
			if(item != null) {
				if(item.gameObject.tag == "okl" || item.gameObject.tag == "pkl" || 
				item.gameObject.tag == "bl") {
					item.gameObject.GetComponent<PlayerKillLaser>().ButtonPressed();
				}
			}
		}
	}
}
