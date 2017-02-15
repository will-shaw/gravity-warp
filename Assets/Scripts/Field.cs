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
        if (objectKilling)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5F, 0.8F, 0.8F, 0.85F);
        }
        if (active)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            GetComponent<Light>().enabled = false;
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
                        GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetLaserKill(), 1);
                        other.gameObject.GetComponent<Animator>().SetBool("hitLaser", true);
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
                    GetComponent<AudioSource>().PlayOneShot(Camera.main.GetComponent<AudioManager>().GetLaserKill(), 1);
                    other.gameObject.GetComponent<Animator>().SetBool("hitLaser", true);
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
            if (currentLinks > 0)
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
                GetComponent<Light>().enabled = true;
                active = !active;
            }
            else if (active)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
                active = !active;
                GetComponent<Light>().enabled = false;
            }
        }
        else
        {
            if (active && currentLinks >= linksRequired)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<Collider2D>().enabled = false;
                active = !active;
                GetComponent<Light>().enabled = false;
            }
            else if (!(active))
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                gameObject.GetComponent<Collider2D>().enabled = true;
                active = !active;
                GetComponent<Light>().enabled = true;
            }
        }
    }

}
