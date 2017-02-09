using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool active = true;
    bool connection;
    public Transform[] activates;
    AudioClip change;

    void Start()
    {
        change = Camera.main.GetComponent<AudioManager>().GetLeverChange();
        GetComponent<Animator>().SetBool("Active", active);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<GlueObject>() == null)
        {
            GetComponent<Animator>().SetBool("Active", !active);
            switch (connection)
            {
                case true:
                    SubLink();
                    connection = false;
                    break;
                case false:
                    AddLink();
                    connection = true;
                    break;
            }
            active = !active;
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
                item.GetComponent<Emit>().toggle();
            }
        }
    }

    void SubLink()
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
                item.GetComponent<Emit>().toggle();
            }
        }
    }

}
