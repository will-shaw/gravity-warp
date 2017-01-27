﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag != "Wall") {
			other.GetComponent<Glue>().gluing();
			Camera.main.GetComponent<CombinedGravityWarp>().glues.Remove(gameObject.GetComponent<Transform>());
			Destroy(gameObject);
		} else {
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		
	}
		
}
