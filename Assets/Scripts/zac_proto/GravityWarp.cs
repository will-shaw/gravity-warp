using UnityEngine;
using UnityEngine.UI;

public class GravityWarp : MonoBehaviour {

	float gravityScale = 4.0f;
	float changeGrav = 0f;
	float changeobject =0f;
	public Text text;

	public Transform[] boxes;

	int counter =0;
	public float thrust;

	public string effected = "Wood";
	string gravity = "D";

	float keyCool =0f;
	string lastGravDir;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)&& changeobject > 0.5f){
			if(effected == "Wood"){
				effected = "Metal";
			}else{
				effected = "Wood";
			}
			changeobject = 0f;
		}else{
			changeobject += Time.deltaTime;
		}
		text.text = boxes[0].GetComponent<Rigidbody2D>().velocity.ToString();
		
		for (int i = 0; i < boxes.Length; i++) {
			if(changeGrav > 5f && keyCool> 0.3f) {
				if(boxes[i].CompareTag(effected)){
					if (Input.GetKey(KeyCode.UpArrow) && boxes[i].GetComponent<boxInfo>().gravity !='U') {
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = -gravityScale;
						boxes[i].GetComponent<boxInfo>().gravity  = 'U';
						counter++;
						keyCool=0f;
					}
					if (Input.GetKey (KeyCode.DownArrow) && boxes[i].GetComponent<boxInfo>().gravity !='D') {
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
						boxes[i].GetComponent<boxInfo>().gravity  = 'D';
						counter++;
						keyCool=0f;
					}
					if (Input.GetKey (KeyCode.LeftArrow)&& boxes[i].GetComponent<boxInfo>().gravity !='R') {
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
						boxes[i].GetComponent<boxInfo>().gravity  = 'R';
						counter++;
						keyCool=0f;
					}
					if (Input.GetKey (KeyCode.RightArrow)&& boxes[i].GetComponent<boxInfo>().gravity !='L') {
						boxes[i].GetComponent<Rigidbody2D> ().gravityScale = 0;
						boxes[i].GetComponent<boxInfo>().gravity = 'L';
						counter++;
						keyCool=0f;
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
			}else{
				changeGrav+= Time.deltaTime;
				keyCool += Time.deltaTime;
			}
			if( counter>4){
				counter =0;
				changeGrav=0f;
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

