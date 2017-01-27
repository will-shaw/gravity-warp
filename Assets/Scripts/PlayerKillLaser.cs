using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillLaser : MonoBehaviour {

	public Sprite offSprite;

	public Sprite onSprite;

	public bool on;
	void OnTriggerEnter2D(Collider2D other)
	{
		if(on){
			if(gameObject.tag == "pkl") {
				if(other.gameObject.tag == "Player") {
					Destroy(other.gameObject);
				}
			} else if (gameObject.tag == "okl") {
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
			if(gameObject.tag == "bl") {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			}
		} else {
			on = true;
			gameObject.GetComponent<SpriteRenderer>().sprite = onSprite;
			if(gameObject.tag == "bl") {
				gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			}
		}
	}
}
