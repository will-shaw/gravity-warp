﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Transform exit_point; // Exit trigger. Triggers load.
    public Transform switch_point; // Scene change trigger.
    public Transform exitDoor; // The exit door. Will as level loads.
    public string nextLevel; // The level to load.
    public bool asyncLoad = true; // Is the load immediate or async.
    bool loadOnce; // Makes sure that Exit trigger can only trigger once.
    AsyncOperation op; // Holds the async operation. Used to 'allowSceneActivation'.

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == switch_point.GetComponent<Collider2D>())
        {
            op.allowSceneActivation = true;
        }
        else if (other == exit_point.GetComponent<Collider2D>())
        {
            if (asyncLoad && !loadOnce)
            {
                loadOnce = true;
                Debug.Log("Loading " + nextLevel);
                op = SceneManager.LoadSceneAsync(nextLevel);
                op.allowSceneActivation = false;
                exitDoor.GetComponent<Door>().ActivateLink(1);
            }
        }
    }

}