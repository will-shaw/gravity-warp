using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelLoader : MonoBehaviour
{

    public Transform entry_point;
    public Transform exit_point;
    public Transform switch_point;
    public Transform player;
    public Transform exitDoor;
    public string nextLevel;
    public bool asyncLoad = true;
    bool once;
    AsyncOperation op;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "switch_point")
        {
            op.allowSceneActivation = true;
        }
        else if (other.tag == "exit_point")
        {
            if (asyncLoad && !once)
            {
                once = true;
                print("Loading " + nextLevel);
                op = SceneManager.LoadSceneAsync(nextLevel);
                op.allowSceneActivation = false;
                exitDoor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                exitDoor.GetComponent<Rigidbody2D>().gravityScale = 4f;
            }
        }
    }

}