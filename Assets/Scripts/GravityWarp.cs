using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWarp : MonoBehaviour {

	float gravityScale = 4.0f;

	public Transform box;

	public float thrust;

	string gravity = "D";

	string lastGravDir;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow) || gravity == "U") {
			box.GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
			gravity = "U";
		}
		if (Input.GetKey (KeyCode.DownArrow) || gravity == "D") {
			box.GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
			gravity = "D";
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			box.GetComponent<Rigidbody2D> ().gravityScale = 0;
			gravity = "R";
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			box.GetComponent<Rigidbody2D> ().gravityScale = 0;
			gravity = "L";
		}
		if (Input.GetKey (KeyCode.Space)) {
			if (gravity == "0") {
				gravity = lastGravDir;
			} else {
				lastGravDir = gravity;
				gravity = "0";
				box.GetComponent<Rigidbody2D> ().gravityScale = 0;
			}
		}
		if (gravity == "L") {
			box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(-box.position.x * thrust, 0));
		}
		if (gravity == "R") {
			box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(box.position.x * thrust, 0));
		}
	}
}
