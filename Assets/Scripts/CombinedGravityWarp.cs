using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CombinedGravityWarp : MonoBehaviour {

	float gravityScale = 4.0f;

	public Text text;

	public Transform[] resources;

	 public List<Transform> boxes = new List<Transform>();

	 public List<Transform> glues = new List<Transform>();

	public float thrust;

	public int glueCount;

	public int boxSize;
	string gravity = "D";

	string lastGravDir;
	// Use this for initialization
	void Start () {
		boxSize =2;
		boxes.Add(Instantiate(resources[0]));
		boxes.Add(Instantiate(resources[1]));
		Transform newPlayer = Instantiate(resources[2]);
		GetComponent<Player>().player = newPlayer;
		GetComponent<CameraZoom>().player = newPlayer;
		boxes.Add(newPlayer);
	}

	// Update is called once per frame
	void Update () {
		doGravity();
		if(glueCount > 0) {
			doGlueGravity();
		}
	}

	void doGlueGravity() {
		foreach(Transform glue in glues){
			if(glue != null) {
					if (Input.GetKey(KeyCode.UpArrow) || gravity == "U") {
						glue.GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
						gravity = "U";
					}
					if (Input.GetKey (KeyCode.DownArrow) || gravity == "D") {
						glue.GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
						gravity = "D";
					}
					if (Input.GetKey (KeyCode.LeftArrow)) {
						glue.GetComponent<Rigidbody2D> ().gravityScale = 0;
						gravity = "R";
					}
					if (Input.GetKey (KeyCode.RightArrow)) {
						glue.GetComponent<Rigidbody2D> ().gravityScale = 0;
						gravity = "L";
					}
					if (gravity == "L") {
						glue.GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
					}
					if (gravity == "R") {
						glue.GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
					}
			}
		}
	}

	void doGravity() {
		foreach(Transform box in boxes){
			if(box != null) {
				if(!(box.GetComponent<CombinedGlue>().isGlued())) {
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
					if (gravity == "L") {
						box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
					}
					if (gravity == "R") {
						box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
					}
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