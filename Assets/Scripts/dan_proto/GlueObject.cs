using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		other.GetComponent<Glue>().gluing();
		
	}
		
}
