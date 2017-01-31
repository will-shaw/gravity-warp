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
			toggleItemStatus();
		}else{
			on = true;
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