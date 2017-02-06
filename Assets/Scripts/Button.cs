using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform[] activates;
    public bool toggleable = true;

    public AudioClip toggleOn;
    public AudioClip toggleOff;
    public AudioClip release;

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
                GetComponent<AudioSource>().PlayOneShot(toggleOn, 1);
            }
            else
            {
                active = true;
                GetComponent<AudioSource>().Play();
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
                GetComponent<AudioSource>().PlayOneShot(release, 1);                
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
        Vector2 currentOffset = GetComponent<BoxCollider2D>().offset;
        Vector2 newOffset;
        switch (wallAttached)
        {
            case "D":
                if (active)
                {
                    transform.Translate(0, -0.4F, 0, 0);
                    newOffset = new Vector2(currentOffset.x, 0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                else
                {
                    transform.Translate(0, 0.4F, 0, 0);
                    newOffset = new Vector2(currentOffset.x, -0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                break;
            case "U":
                if (active)
                {
                    transform.Translate(0, 0.4F, 0, 0);
                    newOffset = new Vector2(currentOffset.x, -0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                else
                {
                    transform.Translate(0, -0.4F, 0, 0);
                    newOffset = new Vector2(currentOffset.x, 0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                break;
            case "L":
                if (active)
                {
                    transform.Translate(-0.4F, 0, 0, 0);
                    newOffset = new Vector2(currentOffset.x, -0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                else
                {
                    transform.Translate(0.4F, 0, 0, 0);
                    newOffset = new Vector2(currentOffset.x, 0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                break;
            case "R":
                if (active)
                {
                    transform.Translate(0.4F, 0, 0, 0);
                    newOffset = new Vector2(currentOffset.x, 0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                else
                {
                    transform.Translate(-0.4F, 0, 0, 0);
                    newOffset = new Vector2(currentOffset.x, -0.4F+currentOffset.y);
                    GetComponent<BoxCollider2D>().offset = newOffset;
                    currentOffset = newOffset;
                }
                break;
        }
    }

}
