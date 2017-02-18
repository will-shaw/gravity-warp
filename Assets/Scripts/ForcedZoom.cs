using UnityEngine;

public class ForcedZoom : MonoBehaviour {

	public bool zoomIn = true;
    public bool zoomOut = false;

    void OnTriggerEnter2D(Collider2D other)
	{
		CameraZoom cameraZoom = Camera.main.GetComponent<CameraZoom>();
		if(other.gameObject.tag == "Player" && Camera.main.orthographicSize == cameraZoom.zoomFar && zoomIn)
		{
			CameraZoom.state = 1;
			CameraZoom.StartLerp();
		}
		else if (other.gameObject.tag == "Player" && Camera.main.orthographicSize == cameraZoom.zoomClose && zoomOut)
		{
			CameraZoom.state = 0;
			CameraZoom.StartLerp();
		}
	}
}
