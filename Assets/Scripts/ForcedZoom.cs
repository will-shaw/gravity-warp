using UnityEngine;

public class ForcedZoom : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
		if(other.gameObject.tag == "Player" && Camera.main.orthographicSize == cameraZoom.zoomFar)
		{
			CameraZoom.state = 1;
			CameraZoom.StartLerp();
		}
		
	}
}
