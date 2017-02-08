using UnityEngine;

public class Field : MonoBehaviour
{

    public bool laser = false;
    public bool active = true;
    public bool objectKilling = true;
    public int linksRequired = 1;

    public bool startOff = false;
    int currentLinks = 0;

    void Start()
    {
        if (active)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (active && laser)
        {
            if (objectKilling)
            {
                if (other.gameObject.tag != "Wall")
                {
                    if (other.gameObject.tag == "Player")
                    {
                        Camera.main.GetComponent<GravityWarp>().playerDead = true;
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                }
            }
            else
            {
                if (other.gameObject.tag == "Player")
                {
                    Camera.main.GetComponent<GravityWarp>().playerDead = true;
                }
            }
        }
    }

    public void ActivateLink(int source)
    {
        if (source == 1)
        {
            currentLinks++;
        }
        else
        {
            currentLinks--;
        }
        ToggleField();
    }

    public void ToggleField()
    {
        if (startOff)
        {
            if (!(active) && currentLinks >= linksRequired)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
                active = !active;
            }
            else if (active)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
                active = !active;
            }
        }
        else
        {
            if (active && currentLinks >= linksRequired)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
                active = !active;
            }
            else if (!(active))
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
                active = !active;
            }
        }
    }

}
