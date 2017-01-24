using UnityEngine;
using UnityEngine.UI;

public class GravityWarp2 : MonoBehaviour {

	float gravityScale = 4.0f;

	public Text text;

	public Transform[] boxes;

	public float thrust;

	public string effected = "Wood";

	public string altEffected = "Metal";
	string gravity = "D";

	string lastGravDir;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)){
			if(effected == "Wood"){
				effected = "Metal";
				altEffected = "Wood";
			}else{
				effected = "Wood";
				altEffected ="Metal";
			}
		}
		text.text = boxes[0].GetComponent<Rigidbody2D>().velocity.ToString();
		for (int i = 0; i < boxes.Length; i++) {
			if(!(boxes[i].GetComponent<Glue>().isGlued())) {
				if(boxes[i].CompareTag(effected)||boxes[i].CompareTag(altEffected)){
					if (Input.GetKey(KeyCode.UpArrow)) {
						if(boxes[i].CompareTag(effected)){
							boxes[i].GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
							boxes[i].GetComponent<boxInfo>().gravity  = 'U';
						}else{
							boxes[i].GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
							boxes[i].GetComponent<boxInfo>().gravity  = 'D';
						}
					}
					if (Input.GetKey (KeyCode.DownArrow)) {
						if(boxes[i].CompareTag(effected)){
							boxes[i].GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
							boxes[i].GetComponent<boxInfo>().gravity  = 'D';
						}else{
							boxes[i].GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
							boxes[i].GetComponent<boxInfo>().gravity  = 'U';
						}
					}
					if (Input.GetKey (KeyCode.LeftArrow)) {
						
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
						if(boxes[i].CompareTag(effected)){
							boxes[i].GetComponent<boxInfo>().gravity  = 'R';
						}else{
							boxes[i].GetComponent<boxInfo>().gravity  = 'L';
						}
					}
					if (Input.GetKey (KeyCode.RightArrow)) {
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
						if(boxes[i].CompareTag(effected)){
							boxes[i].GetComponent<boxInfo>().gravity = 'L';
						}else{
							boxes[i].GetComponent<boxInfo>().gravity  = 'R';
						}
					}
					/*if (Input.GetKey (KeyCode.Space)) {
						if (gravity == "0") {
							gravity = lastGravDir;
						} else {
							lastGravDir = gravity;
							gravity = "0";
							boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
						}
					}*/
				}
				if (boxes[i].GetComponent<boxInfo>().gravity == 'L') {
						boxes[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(4.0f * thrust, 0));
				}
				if (boxes[i].GetComponent<boxInfo>().gravity  == 'R') {
					boxes[i].GetComponent<Rigidbody2D> ().AddForce(new Vector2(-4.0f * thrust, 0));
				}
			}
		}
	}
}
