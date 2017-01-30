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
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));			
		} else if (Input.GetKey(KeyCode.A)) {
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));						
		}

        if (Input.GetKey(KeyCode.W) && Time.realtimeSinceStartup > cooldown + 1)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250.0f));
            cooldown = Time.realtimeSinceStartup;
        }
    }
}