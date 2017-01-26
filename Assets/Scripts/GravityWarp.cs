using UnityEngine;
using UnityEngine.UI;
//using System.Collections.Generic;

public class GravityWarp : MonoBehaviour {

	float gravityScale = 4.0f;

	public Text text;

	public Transform[] resources;

	 public System.Collections.Generic.List<Transform> boxes = new System.Collections.Generic.List<Transform>();

	public float thrust;

	public int boxSize;
	string gravity = "D";

	string lastGravDir;
	// Use this for initialization
	void Start () {
		boxSize =3;
		boxes.Add( Instantiate(resources[0]));
		boxes.Add(Instantiate(resources[1]));
		Transform newPlayer = Instantiate(resources[2]);
		Camera.main.GetComponent<Player>().player = newPlayer;
		boxes.Add(newPlayer);
	}

	// Update is called once per frame
	void Update () {
		//for (int i = 0; i < boxes.Capacity; i++) {
			foreach(Transform box in boxes){
				//Debug.Log(box.GetComponent<Rigidbody2D>().velocity);
			if(!(box.GetComponent<Glue>().isGlued())) {
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
					box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
				}
				if (gravity == "R") {
					box.GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
				}
			}
		}
	}
}
