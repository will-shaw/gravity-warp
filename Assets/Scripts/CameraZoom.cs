﻿using UnityEngine;
public class CameraZoom : MonoBehaviour
{
    Transform cam; // Transform component of the main camera.
    public Transform player;
    public Vector3 levelCenter; // Centerpoint of the entire level.
    static Vector3 eventCenter; // Centerpoint of event to move to.
    public float zoomFar; // Most zoomed OUT camera size.
    public float zoomClose; // Most zoomed IN camera size.
    public float zoomDuration = 0.5f;
    /* Static variables */


    /* Camera Tracking */
    public float trackSpeed; // Speed at which the camera moves.
    public float trackDistance; // Deadzone distance before triggering movement.

    static bool isLerping;
    static float timeStartedLerping;
    public static int state = -1;

    public static bool eventTriggered = false;

    public bool startZoomed = true;

    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
        if (startZoomed)
        {
            Camera.main.orthographicSize = zoomClose;
        } else {
            Camera.main.orthographicSize = zoomFar;            
        }
    }
    void Update()
    {
        if (player != null && GetDistFromCenter() > trackDistance && Camera.main.orthographicSize == zoomClose && !eventTriggered)
        {
            float dx = Mathf.Lerp(cam.position.x, player.position.x, trackSpeed * Time.deltaTime);
            float dy = Mathf.Lerp(cam.position.y, player.position.y, trackSpeed * Time.deltaTime);
            cam.position = new Vector3(dx, dy, -10);
        }
        if (Camera.main.orthographicSize == zoomFar && Input.GetKeyDown(InputManager.zoom))
        {
            state = 1;
            StartLerp();
        }
        if (Camera.main.orthographicSize == zoomClose && Input.GetKeyDown(InputManager.zoom))
        {
            state = 0;
            StartLerp();
            eventTriggered = false;
        }
    }

    void FixedUpdate()
    {
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStartedLerping;
            float percentageComplete = timeSinceStarted / zoomDuration;
            switch (state)
            {
                case 0: // Zoom Out & Move cam to level center.
                    Camera.main.orthographicSize = Mathf.Lerp(zoomClose, zoomFar, percentageComplete);
                    float x = Mathf.Lerp(player.position.x, levelCenter.x, percentageComplete);
                    float y = Mathf.Lerp(player.position.y, levelCenter.y, percentageComplete);
                    cam.position = new Vector3(x, y, -10);
                    break;
                case 1: // Zoom In & Move cam to player.
                    Camera.main.orthographicSize = Mathf.Lerp(zoomFar, zoomClose, percentageComplete);
                    float dx = Mathf.Lerp(levelCenter.x, player.position.x, percentageComplete);
                    float dy = Mathf.Lerp(levelCenter.y, player.position.y, percentageComplete);
                    cam.position = new Vector3(dx, dy, -10);
                    break;
                case 2: // Move cam from player to event.
                    cam.position = Vector3.Lerp(player.position, eventCenter, percentageComplete);
                    break;
                case 3: // Move cam from event to player.
                    cam.position = Vector3.Lerp(eventCenter, player.position, percentageComplete);
                    break;
            }
            if (percentageComplete >= 1.0f)
            {
                isLerping = false;
                state = -1;
            }
        }
    }

    public static void StartLerp()
    {
        if (!isLerping)
        {
            isLerping = true;
            timeStartedLerping = Time.time;
        }
    }

    /* Returns the object distance from the camera center point. */
    float GetDistFromCenter()
    {
        return Mathf.Pow(player.position.x, 2) + Mathf.Pow(player.position.y, 2);
    }

    /* Static callable function to manually pan the camera to a specific location. */
    public static void StartEvent(Vector3 centerPoint)
    {
        eventTriggered = true;
        eventCenter = centerPoint;
        state = 2;
        StartLerp();
    }
    /* Returns the camera to the player; after a StartEvent. */
    public static void StopEvent()
    {
        eventTriggered = false;        
        state = 3;
        StartLerp();
    }
}