using UnityEngine;

public class Door : MonoBehaviour
{
    public int linksRequired = 0;
    public SpriteRenderer[] lights = new SpriteRenderer[2];
    float openPosY;
    float closedPosY;
    int linkCount = 0;
    public bool isOpen = false;
    public bool opensDown = true;
    public bool gravityEnabled = false;

    bool isLerping;
    float timeStartedLerping;
    int state = -1;
    public float openDuration = 0.5f;
    public float height = 5.2f;

    void Start()
    {
        closedPosY = transform.position.y;
        if (opensDown)
        {
            openPosY = transform.position.y - height;
        }
        else
        {
            openPosY = transform.position.y + height;
        }
        if (isOpen)
        {
            transform.position = new Vector3(transform.position.x, openPosY, -9);
        }
        if (gravityEnabled)
        {
            Gravity(true);
        }
    }

    void Update()
    {
        if (transform.position.y <= (closedPosY - (height / 1.5)))
        {
            foreach (SpriteRenderer light in lights)
            {
                light.color = new Color(0, 255, 0);
            }
        } else if (transform.position.y >= (closedPosY - (height / 1.5))) {
            foreach (SpriteRenderer light in lights)
            {
                light.color = new Color(255, 0, 0);
            }
        }
    }


    void FixedUpdate()
    {
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / openDuration;
            float y;
            switch (state)
            {
                case 0: // Door is Closing.
                    y = Mathf.Lerp(openPosY, closedPosY, percentageComplete);
                    transform.position = new Vector3(transform.position.x, y, -9);
                    break;
                case 1: // Door is Opening.
                    y = Mathf.Lerp(closedPosY, openPosY, percentageComplete);
                    transform.position = new Vector3(transform.position.x, y, -9);
                    break;
            }
            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                state = -1;
            }
        }
    }

    void StartLerp()
    {
        if (!isLerping && !gravityEnabled)
        {
            isLerping = true;
            timeStartedLerping = Time.time;
        }
    }

    /* Buttons call this from Enter/Exit Triggers. Allows event driven
       door activation from multiple sources.
    @param source = 0 if origin was EnterTrigger : 1 if from ExitTrigger */
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
            DoorAction(1);
        }
        else
        {
            DoorAction(0);
        }
    }

    public void Gravity(bool b)
    {
        gravityEnabled = b;
        if (b)
        {
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }

    void DoorAction(int s)
    {
        state = s;
        foreach (SpriteRenderer light in lights)
        {
            if (s == 0)
            {
                light.color = new Color(255, 0, 0);
            }
            else
            {
                light.color = new Color(0, 255, 0);
            }
        }
        StartLerp();
    }

}
