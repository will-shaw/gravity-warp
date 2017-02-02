using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Transform exit_point;
    public Transform switch_point;
    public Transform player;
    public Transform exitDoor;
    public string nextLevel;
    public bool asyncLoad = true;
    bool loadOnce;
    AsyncOperation op;

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
                print("Loading " + nextLevel);
                op = SceneManager.LoadSceneAsync(nextLevel);
                op.allowSceneActivation = false;
                exitDoor.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                exitDoor.GetComponent<Rigidbody2D>().gravityScale = 4f;
            }
        }
    }

}