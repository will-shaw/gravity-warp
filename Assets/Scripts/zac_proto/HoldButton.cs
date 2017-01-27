using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : MonoBehaviour {
	bool on = false;

	public Transform[] buttonItems;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag != "Glue") {
			on = true;
			Debug.Log(true);
			toggleItemStatus();
		}
	}

	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag != "Glue") {
			on = false;
			Debug.Log(false);
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
