using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform[] activates;
    public bool toggleable = true;
    public bool active = false;
    //For depression, D is floor, U is roof and L/R are left and right wall.
    public string wallAttached = "D";

    void Start()
    {
        if (toggleable)
        {
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.yellow;
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<GlueObject>() == null)
        {      
            if (toggleable && active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
            Depression();
			Activate(1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(!(toggleable)) {
            if (other.gameObject.GetComponent<GlueObject>() == null)
            {
                active = false;
                Depression();
                Activate(0);
            }
        }
    }

	void Activate(int source) {
		foreach(Transform field in activates) {
			if (field.GetComponent<Field>() != null) {
				field.GetComponent<Field>().ToggleField();
			} else if (field.GetComponent<Door>() != null) {
				field.GetComponent<Door>().ActivateLink(source);
			}
            else if( field.GetComponent<Emit>() != null){
                field.GetComponent<Emit>().toggle();
            }
		}
	}

    void Depression()
    {
        switch (wallAttached)
        {
            case "D":
                if (active)
                {
                    transform.Translate(0, -0.4F, 0, 0);
                }
                else
                {
                    transform.Translate(0, 0.4F, 0, 0);
                }
                break;
            case "U":
                if (active)
                {
                    transform.Translate(0, 0.4F, 0, 0);
                }
                else
                {
                    transform.Translate(0, -0.4F, 0, 0);
                }
                break;
            case "L":
                if (active)
                {
                    transform.Translate(-0.4F, 0, 0, 0);
                }
                else
                {
                    transform.Translate(0.4F, 0, 0, 0);
                }
                break;
            case "R":
                if (active)
                {
                    transform.Translate(0.4F, 0, 0, 0);
                }
                else
                {
                    transform.Translate(-0.4F, 0, 0, 0);
                }
                break;
        }
    }

}
