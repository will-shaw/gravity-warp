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
			toggleItemStatus();
		}
	}

	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag != "Glue") {
			on = false;
			toggleItemStatus();
		}
	}

	void toggleItemStatus() {
		foreach(Transform item in buttonItems) {
			if(item.GetComponent<Laser>() != null) {
				item.gameObject.GetComponent<Laser>().ButtonPressed();
			}
		}
	}
}

