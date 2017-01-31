using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public Sprite offSprite;

	public Sprite onSprite;

	public bool on;

	public bool objectKilling;

	public bool forceField;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(on){
			if(!(objectKilling) && !(forceField)) {
				if(other.gameObject.tag == "Player") {
					Destroy(other.gameObject);
				}
			} else if (objectKilling) {
				if(other.gameObject.tag != "Wall") {
					Destroy(other.gameObject);
				}
			}
		}
	}

	public void ButtonPressed () {
		if(on) {
			on = false;
			gameObject.GetComponent<SpriteRenderer>().sprite = offSprite;
			if(forceField) {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			}
		} else {
			on = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
			if(forceField) {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			}
		}
	}
}
