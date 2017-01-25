using UnityEngine;

public class Player : MonoBehaviour
{

    public Transform bg;
    public Transform player;

    public float speed = 2.0f;

    float cooldown = 0;

    // Update is called once per frame

    void Update()
    {
		float movementInput = 0;
        if (Time.realtimeSinceStartup > cooldown + 1)
        {
            movementInput = Input.GetAxis("Horizontal");
        }
	
		if (Input.GetKey(KeyCode.RightArrow)) {
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * 250f, 0));			
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(Time.deltaTime * -250f, 0));						
		}

		
        //player.Translate(new Vector3(Time.deltaTime * speed * movementInput, 0, 0), Space.World);
        Camera.main.GetComponent<Transform>().position = new Vector3(player.position.x, player.position.y + 2.54f, -10); // Camera follows the player with specified offset position
        bg.position = new Vector3(player.position.x * 0.5f, 0, 2); // Camera follows the player with specified offset position

        if (Input.GetKey(KeyCode.UpArrow) && Time.realtimeSinceStartup > cooldown + 1)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250.0f));
            cooldown = Time.realtimeSinceStartup;
        }

    }
}
