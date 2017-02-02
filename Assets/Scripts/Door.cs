using UnityEngine;

public class Door : MonoBehaviour
{

    int linkCount = 0;
    public int linksRequired = 0;
    public SpriteRenderer[] lights = new SpriteRenderer[2];
    public bool gravityEnabled = false;
    public float openY;
    public float closedY;
    int state = -1;
    public float openSpeed = 4;

    void Start()
    {
        if (gravityEnabled)
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    public void ActivateLink(int source)
    {
        if (source == 1)
        {
            linkCount++;
        }
        else
        {
            linkCount--;
        }

        if (linkCount >= linksRequired)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        state = 1;
        foreach (SpriteRenderer light in lights)
        {
            light.color = new Color(0, 255, 0);
        }
    }
    public void CloseDoor()
    {
        state = 0;
        foreach (SpriteRenderer light in lights)
        {
            light.color = new Color(255, 0, 0);
        }
    }

    public void Gravity(bool value) {
        gravityEnabled = value;
        if (gravityEnabled) {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        } else {
            transform.GetComponent<Rigidbody2D>().gravityScale = 4f; // Magic number remove when possible.
        }
    }

    void Update()
    {
        float y;
        switch (state)
        {
            case -1: // Door stationary.
                break;
            case 0: // Door closing.
                y = Mathf.MoveTowards(transform.position.y, closedY, openSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                break;
            case 1: // Door opening.       
                y = Mathf.MoveTowards(transform.position.y, openY, openSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
                break;
        }
        if ((state == 0 && transform.position.y >= openY) || (state == 1 && transform.position.y <= closedY))
        {
            state = -1;
        }
    }

}
