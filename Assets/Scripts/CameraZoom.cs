using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    Transform cam; // Transform component of the main camera.
    public Transform player;
    public Vector3 levelCenter; // Centerpoint of the entire level.
    public float maxZoom; // Most zoomed in camera size.
    public float minZoom; // Least zoomed in camera size.
    public float zoomSpeed; // Speed at which the camera moves.

    /* Static variables */
    static Vector3 eventCenter; // Centerpoint of event to move to.
    static int moving = 0; // State of the movement.

    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
    }

    void Update()
    {

        if (Camera.main.orthographicSize == maxZoom && Input.GetKeyDown(KeyCode.Tab))
        {
            moving = -1;
        }

        if (Camera.main.orthographicSize == minZoom && Input.GetKeyDown(KeyCode.Tab))
        {
            moving = 1;
        }

        switch (moving)
        {
            case -1:
                Zoom(minZoom);
                Move(player.position.x, player.position.y, cam.position.z);
                break;
            case 1:
                Zoom(maxZoom);
                Move(player.position.x, player.position.y, cam.position.z);
                break;
            case 8:
                print("Moving to event center");
                break;
            default:
                break;
        }

        if (Camera.main.orthographicSize == minZoom || Camera.main.orthographicSize == maxZoom)
        {
            moving = 0;
        }
        if (cam.position == player.position || cam.position == levelCenter)
        {
            moving = 0;
        }

    }

    void Zoom(float target)
    {
        float zoom = Mathf.MoveTowards(Camera.main.orthographicSize, target, zoomSpeed * Time.deltaTime);

        Camera.main.orthographicSize = zoom;
    }

    void Move(float x, float y, float z)
    {
        float dx = Mathf.MoveTowards(cam.position.x, x, zoomSpeed * Time.deltaTime);
        float dy = Mathf.MoveTowards(cam.position.y, y, zoomSpeed * Time.deltaTime);

        cam.position = new Vector3(dx, dy, z);
    }

    static void TriggerEvent(Vector3 centerPoint) {
        eventCenter = centerPoint;
        moving = 8;
    }

}
