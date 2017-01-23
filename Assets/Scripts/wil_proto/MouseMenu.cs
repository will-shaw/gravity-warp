using UnityEngine;

public class MouseMenu : MonoBehaviour {

	public float threshhold = 0.3f;

	public float distance = 300f;

	public Transform menu;
	Transform instantiatedMenu;
	float x;
	float y;

    void OnMouseDown() {
        // Instantiate prefab for visual element of menu.
		instantiatedMenu = Instantiate(menu);

		this.x = Input.mousePosition.x;
		this.y = Input.mousePosition.y;

    }

	void OnMouseUp() {
        // Instantiate prefab for visual element of menu.

		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y;

		float dx = this.x - x;
		float dy = this.y - y;

		if (dx > distance && (dy < Deadzone(dx, 0) && dy > Deadzone(dx, 1))) {
			print("Gravity Left");
			GravityWarp.gravity = "L";
		} else if (dx < -distance && (dy < Deadzone(dx, 1) && dy > Deadzone(dx, 0))) {
			print("Gravity Right");
			GravityWarp.gravity = "R";			
		} else if (dy > distance && (dx < Deadzone(dy, 0) && dx > Deadzone(dy, 1))) {
			print("Gavity Down");
			GravityWarp.gravity = "D";			
		} else if (dy < -distance && (dx < Deadzone(dy, 1) && dx > Deadzone(dy, 0))) {
			print("Gravity Up");
			GravityWarp.gravity = "U";
		}

		Destroy (instantiatedMenu.gameObject);
		
    }

	float Deadzone(float v, int s) {
		if (s == 0) { 
			return v * threshhold;
		} else {
			return v * -threshhold;
		}
	}


}
