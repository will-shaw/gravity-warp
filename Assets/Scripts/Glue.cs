using UnityEngine;

public class Glue : MonoBehaviour {

	bool glued;
	float gluedTime;
	public float glueTime;

	void Start () {
		glued  = false;
	}
	
	void Update () {
		if(glued && gluedTime > 0){
			gluedTime -= Time.deltaTime;
		} else if (glued) {
			glued = false;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;			
		}
		
	}

	public void gluing() {
			glued = true;
			gluedTime = glueTime;
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			Camera.main.GetComponent<CameraZoom>().player.GetComponent<GlueControl>().changeGlueCount(0);
	}

	public bool isGlued (){
		return glued;
	}
}