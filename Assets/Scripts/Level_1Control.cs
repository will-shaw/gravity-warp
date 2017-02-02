using UnityEngine;

public class Level_1Control : MonoBehaviour {

	string[] gravityDirection = {"D","R","L","U"};

	int currentDirection = 1;

	public float gravityChangeTime;

	float gravityChangeTimer;

	bool autoGravity = true;

	// Use this for initialization
	void Start () {
		gravityChangeTimer = gravityChangeTime;
	}
	
	// Update is called once per frame
	void Update () {
		if(autoGravity){
			gravityChangeTimer -= Time.deltaTime;
			if(gravityChangeTimer <= 0){
				if(currentDirection == 4){
					currentDirection = 0;
				}
				GravityWarp.gravity = gravityDirection[currentDirection++];
				gravityChangeTimer = gravityChangeTime;
			} 
		}
		
	}

	public void disableAutoGravity(){
		autoGravity = false;
		Camera.main.GetComponent<GravityWarp>().gravityControlEnabled = true;
	}
}
