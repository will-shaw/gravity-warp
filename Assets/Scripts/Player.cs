using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Transform player;

	public float speed = 2.0f;

	// Update is called once per frame
	void Update () {
		float movementInput = Input.GetAxis("Horizontal");

		player.Translate( new Vector3(Time.deltaTime * speed * movementInput,0,0), Space.World);
		Camera.main.GetComponent<Transform>().position = new Vector3 (player.position.x, player.position.y, -10); // Camera follows the player with specified offset position
	}
}
