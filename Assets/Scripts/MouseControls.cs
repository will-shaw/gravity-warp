using UnityEngine;

public class MouseControls : MonoBehaviour
{
    public float threshhold = 0.3f;
    public float distance = 50f;
    public Transform menu;
    Transform instantiatedMenu;
    float x;
    float y;

    void Update()
    {
        if (InputManager.gravityControlScheme == 0)
        {
            if (Input.GetKeyDown(InputManager.gravityDown) && GetComponent<GravityWarp>().gravityControlEnabled)
            {
                this.x = Input.mousePosition.x;
                this.y = Input.mousePosition.y;

                // Play a sound here. (Quietly).
                // Instantiate the radial menu at the mouse position.
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 4.0f;
                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                instantiatedMenu = Instantiate(menu, objectPos, Quaternion.identity);
            }
            if (Input.GetKeyUp(InputManager.gravityDown) && GetComponent<GravityWarp>().gravityControlEnabled)
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

                if (instantiatedMenu)
                {
                    Destroy(instantiatedMenu.gameObject);
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


}
