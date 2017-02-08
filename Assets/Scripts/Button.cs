using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform[] activates;
    public bool toggleable = true;

    AudioClip toggleOn;
    AudioClip toggleOff;
    AudioClip release;

    public bool active = false;
    //For depression, D is floor, U is roof and L/R are left and right wall.
    public string wallAttached = "D";

    void Start()
    {
        toggleOn = Camera.main.GetComponent<AudioManager>().GetButtonToggle(true);
        toggleOff = Camera.main.GetComponent<AudioManager>().GetButtonToggle(false);
        release = Camera.main.GetComponent<AudioManager>().GetButton();

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
                Depression();
                Activate(0);
            }
            else if (toggleable && !(active))
            {
                active = true;
                GetComponent<AudioSource>().PlayOneShot(toggleOff, 1);
                Depression();
                Activate(1);
            }
            else
            {
                if(other.gameObject.tag == "destructable"){
                    other.gameObject.GetComponent<BoxCollision>().setActiveButton(this.gameObject);
                }
                active = true;
                GetComponent<AudioSource>().Play();
                Depression();
			    Activate(1);
            }
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

	public void Activate(int source) {
		foreach(Transform field in activates) {
			if (field.GetComponent<Field>() != null) {
				field.GetComponent<Field>().ActivateLink(source);
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
