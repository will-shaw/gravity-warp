using UnityEngine;
using UnityEngine.UI;
public class Glue : MonoBehaviour {

	bool glued;
	float gluedTime;
	public float glueTime;
	
	public Transform canvas;
	void Start () {
		glued  = false;
		if(gameObject.GetComponent<Player>() != null){
				canvas = gameObject.GetComponent<Player>().canvas;
			}
	}
	
	
	void Update () {
		if(glued && gluedTime > 0){
			gluedTime -= Time.deltaTime;
			if(gameObject.GetComponent<Player>() != null){
				canvas.FindChild("Text").GetComponent<Text>().text = string.Format("{0:N2}",gluedTime);
			}
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