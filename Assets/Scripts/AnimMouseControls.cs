using UnityEngine;

public class AnimMouseControls : MonoBehaviour {

    public float threshhold = 0.3f;
    public float distance = 50f;
    public Transform menu;

	Animator anim;
	bool controlsActive = false;
	Transform outer;
	Transform inner;
    float x;
    float y;

	void Start() {
		outer = menu.GetChild(1);
		inner = menu.GetChild(0);
		//anim = GetComponent<Animator>();
	}

    void Update()
    {
        if (InputManager.gravityControlScheme == 0)
        {
			if(Camera.main.GetComponent<GravityWarp>().gravityControlEnabled)
			{
				switch(GravityWarp.gravity) {
					case "U" :
						SetInnerColor(0);
						break;
					case "D" :
						SetInnerColor(2);
						break;
					case "L" :
						SetInnerColor(1);
						break;
					case "R" :
						SetInnerColor(3);
						break;
				}
			}
			if(controlsActive)
			{
				float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;

                float dx = this.x - x;
                float dy = this.y - y;

                if (dx > distance && (dy < Deadzone(dx, 0) && dy > Deadzone(dx, 1)))
                {
                    SetActiveDirection(2);
					//anim.SetInteger("Direction", 2);
                }
                else if (dx < -distance && (dy < Deadzone(dx, 1) && dy > Deadzone(dx, 0)))
                {
                    SetActiveDirection(3);
					//anim.SetInteger("Direction", 3);
                }
                else if (dy > distance && (dx < Deadzone(dy, 0) && dx > Deadzone(dy, 1)))
                {
                    SetActiveDirection(1);
					//anim.SetInteger("Direction", 1);
                }
                else if (dy < -distance && (dx < Deadzone(dy, 1) && dx > Deadzone(dy, 0)))
                {
                    SetActiveDirection(0);
					//anim.SetInteger("Direction", 0);
                }
				else
				{
					SetActiveDirection(4);
					//anim.SetInteger("Direction", 4);
				}
			}
            if (Input.GetKeyDown(InputManager.gravityDown) && Camera.main.GetComponent<GravityWarp>().gravityControlEnabled)
            {
                this.x = Input.mousePosition.x;
                this.y = Input.mousePosition.y;

                // Play a sound here. (Quietly).
                // Instantiate the radial menu at the mouse position.
                Vector3 mousePos = Input.mousePosition;
                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
				objectPos.x += -12.2F;
				objectPos.y += -9.7F;
                objectPos.z = -13;
                menu.transform.position = objectPos;
				outer.gameObject.SetActive(true);
				inner.gameObject.SetActive(true);
				controlsActive = true;
            }
            if (Input.GetKeyUp(InputManager.gravityDown) && Camera.main.GetComponent<GravityWarp>().gravityControlEnabled)
            {
                float x = Input.mousePosition.x;
                float y = Input.mousePosition.y;

                float dx = this.x - x;
                float dy = this.y - y;

                if (dx > distance && (dy < Deadzone(dx, 0) && dy > Deadzone(dx, 1)))
                {
                    GravityWarp.gravity = "L";
                }
                else if (dx < -distance && (dy < Deadzone(dx, 1) && dy > Deadzone(dx, 0)))
                {
                    GravityWarp.gravity = "R";
                }
                else if (dy > distance && (dx < Deadzone(dy, 0) && dx > Deadzone(dy, 1)))
                {
                    GravityWarp.gravity = "D";
                }
                else if (dy < -distance && (dx < Deadzone(dy, 1) && dx > Deadzone(dy, 0)))
                {
                    GravityWarp.gravity = "U";
                }

                if (inner.gameObject.activeInHierarchy == true)
                {
                    outer.gameObject.SetActive(false);
					inner.gameObject.SetActive(false);
					controlsActive = false;
                }
            }
        }
        
    }

    float Deadzone(float v, int s)
    {
        if (s == 0)
        {
            return v * threshhold;
        }
        else
        {
            return v * -threshhold;
        }
    }

	void SetActiveDirection(int direction) 
	{
		for(int i = 0; i < 4; i++){
			outer.GetChild(i).gameObject.SetActive(false);
		}
		if(direction < 4) {
			outer.GetChild(direction).gameObject.SetActive(true);
		}
	}

	void SetInnerColor(int direction)
	{
		for(int i = 0; i < 4; i++){
			inner.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		}
		inner.GetChild(direction).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
	}


}
