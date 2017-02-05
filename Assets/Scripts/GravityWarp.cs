using UnityEngine;
using System.Collections.Generic;

public class GravityWarp : MonoBehaviour
{

    public static float gravityScale = 4.0f; // Amount of gravity to apply.
    public List<Transform> boxes = new List<Transform>();
    public List<Transform> glues = new List<Transform>();
    public List<Transform> clutter = new List<Transform>();
    public float thrust; // For horizontal movement. Multiplies gravityScale.
    public static string gravity = "D"; // The current gravity direction.

    public Transform player;
    bool hasRemote;

    public GameObject deadMenu;
    public bool playerDead = false;
    public bool gravityControlEnabled;

    float reTimer = 0f;
    public float coolDown = 0f;
    int gravityCount = 0;
    void Update()
    {
        // Check for gravity change.
        if (gravityControlEnabled)
        {
            InputHandler();
            if (!hasRemote)
            {
                hasRemote = true;
                player.GetComponent<Animator>().SetBool("hasRemote", hasRemote);
            }
        }
        else if (hasRemote)
        {
            hasRemote = false;
            player.GetComponent<Animator>().SetBool("hasRemote", hasRemote);
        }

        /* Updates box gravity. The player is also added to this list by Player.cs */
        BoxGravity();
        // If some glue exists, update glue gravity.
        GlueGravity();
        ClutterGravity();
        if (playerDead)
        {
            deadMenu.GetComponent<DieMenuHandler>().ShowPause();
        }
    }

    /* Handles user input for gravity change. */
    void InputHandler()
    {
        if (gravityCount < 5)
        {
            if (Input.GetKey(KeyCode.UpArrow) && gravity != "U")
            {
                gravity = "U";
                gravityCount++;
                reTimer = 0f;
            }
            if (Input.GetKey(KeyCode.DownArrow) && gravity != "D")
            {
                gravity = "D";
                gravityCount++;
                reTimer = 0f;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && gravity != "L")
            {
                gravity = "L";
                gravityCount++;
                reTimer = 0f;
            }
            if (Input.GetKey(KeyCode.RightArrow) && gravity != "R")
            {
                gravity = "R";
                gravityCount++;
                reTimer = 0f;
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

    /* Controls gravity for all items in the glues list. */
    void GlueGravity()
    {
        foreach (Transform glue in glues)
        {
            if (glue != null)
            {
                switch (gravity)
                {
                    case "U":
                        glue.GetComponent<Rigidbody2D>().gravityScale = -gravityScale;
                        break;
                    case "D":
                        glue.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                        break;
                    case "L":
                        glue.GetComponent<Rigidbody2D>().gravityScale = 0;
                        glue.GetComponent<Rigidbody2D>().AddForce(new Vector2(-gravityScale * thrust, 0));
                        break;
                    case "R":
                        glue.GetComponent<Rigidbody2D>().gravityScale = 0;
                        glue.GetComponent<Rigidbody2D>().AddForce(new Vector2(gravityScale * thrust, 0));
                        break;
                }
            }
        }
    }

    /* Controls gravity for all items in the boxes list. */
    void BoxGravity()
    {
        foreach (Transform box in boxes)
        {
            if (box != null)
            {
                if (!(box.GetComponent<Glue>().isGlued()))
                {
                    switch (gravity)
                    {
                        case "U":
                            box.GetComponent<Rigidbody2D>().gravityScale = -gravityScale;
                            break;
                        case "D":
                            box.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                            break;
                        case "L":
                            box.GetComponent<Rigidbody2D>().gravityScale = 0;
                            box.GetComponent<Rigidbody2D>().AddForce(new Vector2(-gravityScale * thrust, 0));
                            break;
                        case "R":
                            box.GetComponent<Rigidbody2D>().gravityScale = 0;
                            box.GetComponent<Rigidbody2D>().AddForce(new Vector2(gravityScale * thrust, 0));
                            break;
                    }
                }
            }
        }
    }

    void ClutterGravity()
    {
        foreach (Transform item in clutter)
        {
            if (item.GetComponent<Rigidbody2D>() != null)
            {
                switch (gravity)
                {
                    case "U":
                        item.GetComponent<Rigidbody2D>().gravityScale = -gravityScale;
                        break;
                    case "D":
                        item.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
                        break;
                    case "L":
                        item.GetComponent<Rigidbody2D>().gravityScale = 0;
                        item.GetComponent<Rigidbody2D>().AddForce(new Vector2(-gravityScale * thrust, 0));
                        break;
                    case "R":
                        item.GetComponent<Rigidbody2D>().gravityScale = 0;
                        item.GetComponent<Rigidbody2D>().AddForce(new Vector2(gravityScale * thrust, 0));
                        break;
                }
            }
        }
    }
}