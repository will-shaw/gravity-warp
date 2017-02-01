using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform bg;
    public Transform player;
    public float speed = 2.0f;
    float cooldown = 0;

    void Update()
    {
		if (Input.GetKey(KeyCode.D)) {
			//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));
            if(GravityWarp.gravity == "R") {
                player.transform.Translate(new Vector3(0, Time.deltaTime * speed * -1,0), Space.World);
            } else if (GravityWarp.gravity == "U" || GravityWarp.gravity == "D"){
                player.transform.Translate(new Vector3(Time.deltaTime * speed,0,0), Space.World);
            } else if (GravityWarp.gravity == "L"){
                player.transform.Translate(new Vector3(0, Time.deltaTime * speed,0), Space.World);
            } else if (GravityWarp.gravity == "U"){
                player.transform.Translate(new Vector3(Time.deltaTime * speed,0,0), Space.World);
            }		
		} else if (Input.GetKey(KeyCode.A)) {
			//player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));
            if(GravityWarp.gravity == "R") {
                player.transform.Translate(new Vector3(0, Time.deltaTime * speed,0), Space.World);
            } else if (GravityWarp.gravity == "U" || GravityWarp.gravity == "D"){
                player.transform.Translate(new Vector3(Time.deltaTime * speed * -1,0,0), Space.World);
            } else if (GravityWarp.gravity == "L"){
                player.transform.Translate(new Vector3(0, Time.deltaTime * speed * -1,0), Space.World);
            }								
		}

        if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
        {
            if(GravityWarp.gravity == "D") {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250.0f));
            } else if(GravityWarp.gravity == "U") {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -250.0f));
            } else if(GravityWarp.gravity == "L") {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(-250.0f, 0));
            } else if(GravityWarp.gravity == "R") {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(250.0f, 0));
            } 
            cooldown = Time.realtimeSinceStartup;
        }
    }
}