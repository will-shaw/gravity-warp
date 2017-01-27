using UnityEngine;

public class MouseMenu : MonoBehaviour
{

    public float threshhold = 0.3f;

    public float distance = 300f;

    public Transform indicator;

    Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow };

    int gColorIndex = 0;

    public Transform menu;
    Transform instantiatedMenu;
    float x;
    float y;

	void Start() {
        indicator.GetComponent<SpriteRenderer>().color = colors[0];
	}

    void OnMouseDown()
    {

        this.x = Input.mousePosition.x;
        this.y = Input.mousePosition.y;

        // Instantiate the radial menu at the mouse position.
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 4.0f;
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        instantiatedMenu = Instantiate(menu, objectPos, Quaternion.identity);

    }

    void OnMouseUp()
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

        Destroy(instantiatedMenu.gameObject);

    }

    void Update()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            if (gColorIndex > 0)
            {
                indicator.GetComponent<SpriteRenderer>().color = colors[--gColorIndex];
            }
            else
            {
                gColorIndex = 3;
                indicator.GetComponent<SpriteRenderer>().color = colors[gColorIndex];
            }
        }
        else if (d < 0f)
        {
            if (gColorIndex < 3)
            {
                indicator.GetComponent<SpriteRenderer>().color = colors[++gColorIndex];
            }
            else
            {
                gColorIndex = 0;
                indicator.GetComponent<SpriteRenderer>().color = colors[gColorIndex];
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
