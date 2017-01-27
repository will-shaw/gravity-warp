using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedGlueObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag != "Wall") {
			other.GetComponent<CombinedGlue>().gluing();
			Destroy(gameObject);
		} else {
			GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
		}
		
	}
		
}