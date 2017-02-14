using UnityEngine;
using UnityEngine.UI;

public class GravityWarp : MonoBehaviour
{

    public static float gravityScale = 3.5f; // Amount of gravity to apply.
    public Transform[] bloods;
    public static string gravity = "D"; // The current gravity direction.

    public Transform player;
    public GameObject menu;
    public bool playerDead = false;
    public bool gravityControlEnabled = false;

    GameObject checkpointText;
    GameObject pauseTimerText;
    GameObject deathTimerText;
    bool blood = false;
    bool hasRemote;

    public float changetmr = 0.0f;
    float reTimer = 0f;
    float deathTimer = 0f;
    int gravityCount = 0;

    public float checktmr = 0f;
    public float coolDown = 0f;
    public bool time = true;
    public float leveltmr = 0f;

    void Start()
    {
        checkpointText = menu.transform.FindChild("txtCheck").gameObject;
        deathTimerText = menu.transform.FindChild("DeathPanel").FindChild("deathTimerText").gameObject;
        pauseTimerText = menu.transform.FindChild("PausePanel").FindChild("pauseTimerText").gameObject;
    }

    void Update()
    {
        changetmr += Time.deltaTime;
        if (time)
        {
            leveltmr += Time.deltaTime;
            pauseTimerText.GetComponent<Text>().text = "Current Level Time: " + string.Format("{0:N2}", leveltmr);
            deathTimerText.GetComponent<Text>().text = "Your Time: " + string.Format("{0:N2}", leveltmr);
        }
        if (checktmr > 0)
        {
            checktmr -= Time.deltaTime;
        }
        else
        {
            checkpointText.SetActive(false);
        }
        // Check for gravity change.
        if (gravityControlEnabled)
        {
            if (InputManager.gravityControlScheme == 1)
            {
                InputHandler();
            }
            if (!hasRemote)
            {
                hasRemote = true;
            }
        }
        else if (hasRemote)
        {
            hasRemote = false;
        }

        switch (gravity)
        {
            case "D":
                Physics2D.gravity = new Vector2(0, gravityScale * -9.81f);
                break;
            case "U":
                Physics2D.gravity = new Vector2(0, gravityScale * 9.81f);
                break;
            case "L":
                Physics2D.gravity = new Vector2(gravityScale * -9.81f, 0);
                break;
            case "R":
                Physics2D.gravity = new Vector2(gravityScale * 9.81f, 0);
                break;
        }

        if (playerDead)
        {
            time = false;
            gravityControlEnabled = false;
            if (!blood)
            {
                if (gravity == "U")
                {
                    Transform bloodSplatter = GameObject.Instantiate(bloods[3]);
                    bloodSplatter.position = player.position;
                }
                else if (gravity == "D")
                {
                    Transform bloodSplatter = GameObject.Instantiate(bloods[1]);
                    bloodSplatter.position = player.position;
                }
                else if (gravity == "L")
                {
                    Transform bloodSplatter = GameObject.Instantiate(bloods[0]);
                    bloodSplatter.position = player.position;
                }
                else if (gravity == "R")
                {
                    Transform bloodSplatter = GameObject.Instantiate(bloods[2]);
                    bloodSplatter.position = player.position;
                }
                blood = true;

            }
            if (deathTimer < 1f)
            {
                deathTimer += Time.deltaTime;

            }
            else
            {
                menu.GetComponent<MenuHandler>().ShowDeath();
            }
        }
    }

    /* Handles user input for gravity change. */
    void InputHandler()
    {
        if (gravityCount < 6)
        {
            if (Input.GetKey(InputManager.gravityUp) && gravity != "U")
            {
                gravity = "U";
                gravityCount++;
                reTimer = 0f;
                player.GetComponent<Player>().antiPhase();
                changetmr = 0.0f;
            }
            if (Input.GetKey(InputManager.gravityDown) && gravity != "D")
            {
                gravity = "D";
                gravityCount++;
                reTimer = 0f;
                player.GetComponent<Player>().antiPhase();
                changetmr = 0.0f;
            }
            if (Input.GetKey(InputManager.gravityLeft) && gravity != "L")
            {
                gravity = "L";
                gravityCount++;
                reTimer = 0f;
                player.GetComponent<Player>().antiPhase();
                changetmr = 0.0f;
            }
            if (Input.GetKey(InputManager.gravityRight) && gravity != "R")
            {
                gravity = "R";
                gravityCount++;
                reTimer = 0f;
                player.GetComponent<Player>().antiPhase();
                changetmr = 0.0f;
            }
            if (gravityCount > 0)
            {
                reTimer += Time.deltaTime;
                if (reTimer > 2f)
                {
                    reTimer = 0;
                    gravityCount = 0;
                }
            }

        }
        else
        {
            if (coolDown > 2f)
            {
                coolDown = 0f;
                gravityCount = 0;
            }
            else
            {
                coolDown += Time.deltaTime;
            }
        }

    }
}