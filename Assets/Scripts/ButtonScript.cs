using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public Transform[] activates;
    AudioClip release;
    bool connection;

    void Start()
    {
        release = Camera.main.GetComponent<AudioManager>().GetButton();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GlueObject>() == null && other.tag != "Wall")
        {
            if(other.tag  == "destructable") {
                other.GetComponent<BoxCollision>().setActiveButton(gameObject);
            }
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetBool("Active", true);
            AddLink();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<GlueObject>() == null && other.tag != "Wall")
        {
            if(other.tag  == "destructable") {
                other.GetComponent<BoxCollision>().setActiveButton(null);
            }
            GetComponent<Animator>().SetBool("Active", false);
            GetComponent<AudioSource>().PlayOneShot(release, 1);
            SubLink();
        }
    }

    void AddLink()
    {
        foreach (Transform item in activates)
        {
            if (item.GetComponent<Door>() != null)
            {
                item.GetComponent<Door>().ActivateLink(1);
            }
            if (item.GetComponent<Field>() != null)
            {
                item.GetComponent<Field>().ActivateLink(1);
            }
            if (item.GetComponent<Emit>() != null)
            {
                item.GetComponent<Emit>().toggle(1);
            }
        }
    }

    public void SubLink()
    {
        foreach (Transform item in activates)
        {
            if (item.GetComponent<Door>() != null)
            {
                item.GetComponent<Door>().ActivateLink(0);
            }
            if (item.GetComponent<Field>() != null)
            {
                item.GetComponent<Field>().ActivateLink(0);
            }
            if (item.GetComponent<Emit>() != null)
            {
                item.GetComponent<Emit>().toggle(0);
            }
        }
    }

}