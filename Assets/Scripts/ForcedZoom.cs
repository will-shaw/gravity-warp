using UnityEngine;

public class ForcedZoom : MonoBehaviour {

	public bool zoomIn = true;

	void OnTriggerEnter2D(Collider2D other)
	{
		CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
		if(other.gameObject.tag == "Player" && Camera.main.orthographicSize == cameraZoom.zoomFar && zoomIn)
		{
			CameraZoom.state = 1;
			CameraZoom.StartLerp();
		}
		else if (other.gameObject.tag == "Player" && Camera.main.orthographicSize == cameraZoom.zoomClose)
		{
			CameraZoom.eventTriggered = false;
		}
	}
}
