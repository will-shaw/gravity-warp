using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    Transform cam; // Transform component of the main camera.
    public Transform player;
    public Vector3 levelCenter; // Centerpoint of the entire level.
    public float zoomFar; // Most zoomed OUT camera size.
    public float zoomClose; // Most zoomed IN camera size.
    public float zoomSpeed; // Speed at which the camera moves.
    /* Static variables */
    static Vector3 eventCenter; // Centerpoint of event to move to.
    static int move = 0; // State of the movement.
    /* Camera Tracking */
    public float trackingDistance; // Deadzone distance before triggering movement.
    public float trackingSpeed; // Speed at which the camera follows.
    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
        Camera.main.orthographicSize = zoomClose;
    }
    void Update()
    {
        if (GetDistFromCenter(player.position.x, player.position.y) > trackingDistance && Camera.main.orthographicSize == zoomClose)
        {
            Move(player.position.x, player.position.y, cam.position.z);
        }
        // If camera is currently zoomed out, pressing tab should zoom in.
        if (Camera.main.orthographicSize == zoomFar && Input.GetKeyDown(KeyCode.Tab))
        {
            move = -1;
        }
        // If camera is currently zoomed in, pressing tab should zoom out.
        if (Camera.main.orthographicSize == zoomClose && Input.GetKeyDown(KeyCode.Tab))
        {
            move = 1;
        }
        // If player is null, stop trying to modify the camera at all.
        if (player != null && move != 0)
        {
            switch (move)
            {
                case -1:
                    Zoom(zoomClose);
                    Move(player.position.x, player.position.y, cam.position.z);
                    break;
                case 1:
                    Zoom(zoomFar);
                    Move(levelCenter.x, levelCenter.y, cam.position.z);
                    break;
                case 8:
                    Zoom(zoomClose);
                    Move(eventCenter.x, eventCenter.y, cam.position.z);
                    break;
                default:
                    break;
            }
            if ((Camera.main.orthographicSize == zoomClose || Camera.main.orthographicSize == zoomFar) &&
                        ((cam.position.x == player.position.x && cam.position.y == player.position.y) ||
                        cam.position == levelCenter || cam.position == eventCenter))
            {
                move = 0;
            }
        }
        else
        {
            move = 0;
        }
    }
    /* Zooms camera to the desired level. Can be any float value, but this uses only two. */
    void Zoom(float target)
    {
        float zoom = Mathf.MoveTowards(Camera.main.orthographicSize, target, zoomSpeed * Time.deltaTime);
        Camera.main.orthographicSize = zoom;
    }
    /* Returns the object distance from the camera center point. */
    float GetDistFromCenter(float x, float y)
    {
        return Mathf.Pow(player.position.x, 2) + Mathf.Pow(player.position.y, 2);
    }
    /* Pans the camera to the given position. Really is a Vector3, but
     splitting them up allows for any offsets to be added prior to the Move call.*/
    void Move(float x, float y, float z)
    {
        float dx = Mathf.MoveTowards(cam.position.x, x, zoomSpeed * Time.deltaTime);
        float dy = Mathf.MoveTowards(cam.position.y, y, zoomSpeed * Time.deltaTime);
        cam.position = new Vector3(dx, dy, z);
    }
    /* Static callable function to manually pan the camera to a specific location. */
    static void StartEvent(Vector3 centerPoint)
    {
        eventCenter = centerPoint;
        move = 8;
    }
    /* Returns the camera to the player; after a StartEvent. */
    static void StopEvent()
    {
        move = -1;
    }
}