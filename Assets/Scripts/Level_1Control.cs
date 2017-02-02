using UnityEngine;

public class Level_1Control : MonoBehaviour {

	//Contains the order in which gravity automatically changes.
	string[] gravityDirection = {"D","R","L","U"};

	//Index of next gravity direction.
	int nextDirection = 1;

	//How often gravity changes
	public float gravityChangeTime;

	//How long till gravity changes
	float gravityChangeTimer;

	//Is gravity being automated
	bool autoGravity = true;

	//Sets timer to wait time.
	void Start () {
		gravityChangeTimer = gravityChangeTime;
	}
	//If gravity is automated, change gravity after certain amount of time.
	void Update () {
		if(autoGravity){
			gravityChangeTimer -= Time.deltaTime;
			if(gravityChangeTimer <= 0){
				if(nextDirection == 4){
					nextDirection = 0;
				}
				GravityWarp.gravity = gravityDirection[nextDirection++];
				gravityChangeTimer = gravityChangeTime;
			} 
		}
		
	}

	//Disables automated gravity and activates player controled gravity
	public void disableAutoGravity(){
		autoGravity = false;
		Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
	}
}
