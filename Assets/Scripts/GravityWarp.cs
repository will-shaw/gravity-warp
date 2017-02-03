using UnityEngine;
using System.Collections.Generic;

public class GravityWarp : MonoBehaviour
{

    public static float gravityScale = 4.0f; // Amount of gravity to apply.
    public List<Transform> boxes = new List<Transform>();
    public List<Transform> glues = new List<Transform>();
    public float thrust; // For horizontal movement. Multiplies gravityScale.
    public static string gravity = "D"; // The current gravity direction.

    public GameObject deadMenu;
    public bool playerDead = false;
    public bool gravityControlEnabled;

    public float coolDown =0f;
    int gravityCount = 0;
    void Update()
    {
        // Check for gravity change.
        if(gravityControlEnabled){
            InputHandler();
        }
        /* Updates box gravity. The player is also added to this list by Player.cs */
        BoxGravity();
        // If some glue exists, update glue gravity.
        GlueGravity();
        if(playerDead){
            deadMenu.GetComponent<DieMenuHandler>().ShowPause();
        }
    }

    /* Handles user input for gravity change. */
    void InputHandler()
    {   
        if(gravityCount <5){
            if (Input.GetKey(KeyCode.UpArrow) && gravity != "U")
            {
                gravity = "U";
                gravityCount++;
            }
            if (Input.GetKey(KeyCode.DownArrow)&& gravity != "D")
            {
                gravity = "D";
                gravityCount++;
            }
            if (Input.GetKey(KeyCode.LeftArrow)&& gravity != "L")
            {
                gravity = "L";
                gravityCount++;
            }
            if (Input.GetKey(KeyCode.RightArrow)&& gravity != "R")
            {
                gravity = "R";
                gravityCount++;
            }
        }
        else{
            if(coolDown>2f){
                coolDown = 0f;
                gravityCount= 0;
            }else{
                coolDown+= Time.deltaTime;
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
}