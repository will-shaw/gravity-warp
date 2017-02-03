using UnityEngine;

public class Field : MonoBehaviour {

	public bool laser = false;
	public bool active = true;
	public bool objectKilling = true;

	void Start () {
		if (active) {
			gameObject.GetComponent<Renderer>().enabled = true;
		} else {
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Collider2D>().enabled = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (active && laser){
			if(objectKilling) {
				if(other.gameObject.tag != "Wall") {
					Destroy(other.gameObject);
				}
			} else {
				if(other.gameObject.tag == "Player") {
					Destroy(other.gameObject);
					Camera.main.GetComponent<GravityWarp>().playerDead = true;
				}
			}
		}
	}

	public void ToggleField() {
		if (active) {		
			gameObject.GetComponent<Renderer>().enabled = false;
			gameObject.GetComponent<Collider2D>().enabled = false;
		} else {
			gameObject.GetComponent<Renderer>().enabled = true;
			gameObject.GetComponent<Collider2D>().enabled = true;
		}
		active = !active;
	}

}
