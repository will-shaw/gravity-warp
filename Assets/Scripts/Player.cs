using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bg;
    public Transform player;
    public float speed = 3.0f;
    float cooldown = 0;

    public string gravity = "D";
    GravityWarp test; 

    void start(){
    //test  = gameObject.GetComponent<GravityWarp>();

    }
    void Update()
    {   
        //GravityWarp test  = gameObject.GetComponent<GravityWarp>();
//        gravity = test.gravity;
        if(gameObject.GetComponent<GravityWarp>().gravity == "D"){
            if (Input.GetKey(KeyCode.D)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));			
            } else if (Input.GetKey(KeyCode.A)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));						
            }

            if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 600.0f));
                cooldown = Time.realtimeSinceStartup;
            }
        }
        if(gameObject.GetComponent<GravityWarp>().gravity == "U"){
            if (Input.GetKey(KeyCode.D)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));			
            } else if (Input.GetKey(KeyCode.A)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));						
            }

            if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -600.0f));
                cooldown = Time.realtimeSinceStartup;
            }
        }
        if(gameObject.GetComponent<GravityWarp>().gravity == "L"){
            if (Input.GetKey(KeyCode.D)) {
                 player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Time.deltaTime * 250f));			
            } else if (Input.GetKey(KeyCode.A)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Time.deltaTime * -250f));							
            }

            if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-400.0f, 0));
                cooldown = Time.realtimeSinceStartup;
            }
        }
        if(gameObject.GetComponent<GravityWarp>().gravity == "R"){
            if (Input.GetKey(KeyCode.D)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Time.deltaTime * -250f));			
            } else if (Input.GetKey(KeyCode.A)) {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Time.deltaTime * 250f));						
            }

            if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(400.0f, 0));
                cooldown = Time.realtimeSinceStartup;
            }
        }
    }

    
}