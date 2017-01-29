﻿using UnityEngine;

public class CameraTrack : MonoBehaviour
{

    public Transform player; // Player is set when instantiated.

    Transform cam;

    public float trackingDistance;

    public float trackingSpeed;

    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		if (GetDistFromCenter(player.position.x, player.position.y) > trackingDistance && Camera.main.orthographicSize == GetComponent<CameraZoom>().zoomClose) {
			Move(player.position.x, player.position.y, cam.position.z);         			
		}
    }

	float GetDistFromCenter(float x, float y) {
		return Mathf.Pow(player.position.x, 2) + Mathf.Pow(player.position.y, 2);
	}

    void Move(float x, float y, float z)
    {
        float dx = Mathf.MoveTowards(cam.position.x, x, trackingSpeed * Time.deltaTime);
        float dy = Mathf.MoveTowards(cam.position.y, y, trackingSpeed * Time.deltaTime);

        cam.position = new Vector3(dx, dy, z);
    }

}
