using UnityEngine;

public class Door : MonoBehaviour
{
    int linkCount = 0;
    public int linksRequired = 0;
    public SpriteRenderer[] lights = new SpriteRenderer[2];
    public bool gravityEnabled = false;
    public float openY;
    public float closedY;

    static bool isLerping;
    static float timeStartedLerping;
    static int state = -1;
    public float openDuration = 0.5f;


    void Start()
    {
        if (!gravityEnabled)
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
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
                    y = Mathf.Lerp(openY, closedY, percentageComplete);
                    transform.position = new Vector2(transform.position.x, y);
                    break;
                case 1: // Door is Opening.
                    y = Mathf.Lerp(closedY, openY, percentageComplete);
                    transform.position = new Vector2(transform.position.x, y);
                    break;
            }
            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                state = -1;
            }
        }
    }

    static void StartLerp()
    {
        if (!isLerping)
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
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    /* Sets lights green : Opens door */
    public void OpenDoor()
    {
        state = 1;
        foreach (SpriteRenderer light in lights)
        {
            light.color = new Color(0, 255, 0);
        }
        StartLerp();
    }
    /* Sets lights red : Closes door */
    public void CloseDoor()
    {
        state = 0;
        foreach (SpriteRenderer light in lights)
        {
            light.color = new Color(255, 0, 0);
        }
        StartLerp();
    }

    /* Sets door gravity to given bool value. */
    public void Gravity(bool value)
    {
        gravityEnabled = value;
        if (gravityEnabled)
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else
        {
            transform.GetComponent<Rigidbody2D>().gravityScale = GravityWarp.gravityScale;
        }
    }

}
