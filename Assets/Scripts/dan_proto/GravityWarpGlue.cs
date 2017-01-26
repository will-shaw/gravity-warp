using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GravityWarpGlue : MonoBehaviour {

	float gravityScale = 4.0f;

	public Text text;

	public Transform[] boxes;

	public float thrust;

	public Transform[] glue;

	public int glueCount;


	string gravity = "D";

	string lastGravDir;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		text.text = boxes[0].GetComponent<Rigidbody2D>().velocity.ToString();
		doGravity();
		if(glueCount > 0) {
			doGravityGlue();
		}
	}

	void doGravity () {
		for (int i = 0; i < boxes.Length; i++) {
			if(!(boxes[i].GetComponent<Glue>().isGlued())) {
				if (Input.GetKey(KeyCode.UpArrow) || gravity == "U") {
					boxes[i].GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
					gravity = "U";
				}
				if (Input.GetKey (KeyCode.DownArrow) || gravity == "D") {
					boxes[i].GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
					gravity = "D";
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
					boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
					gravity = "R";
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
					boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
					gravity = "L";
				}
				if (gravity == "L") {
					boxes[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
				}
				if (gravity == "R") {
					boxes[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
				}
			}
		}
	}

	void doGravityGlue() {
		for (int i = 0; i < glueCount; i++) {
			if(glue[i] != null) {
				if (Input.GetKey(KeyCode.UpArrow) || gravity == "U") {
					glue[i].GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
					gravity = "U";
				}
				if (Input.GetKey (KeyCode.DownArrow) || gravity == "D") {
					glue[i].GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
					gravity = "D";
				}
				if (Input.GetKey (KeyCode.LeftArrow)) {
					glue[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
					gravity = "R";
				}
				if (Input.GetKey (KeyCode.RightArrow)) {
					glue[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
					gravity = "L";
				}
				if (gravity == "L") {
					glue[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
				}
				if (gravity == "R") {
					glue[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
				}
			}
		}
	}

	public void changeGlueCount(int i) {
		if(i == 0) {
			glueCount--;
		} else {
			glueCount++;
		}
	}

	public int getGlueCount() {
		return glueCount;
	}
}
