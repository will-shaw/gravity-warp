using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

	public Transform entry_point;
	public Transform exit_point;
	public Transform player;

	public static Vector2 playerPos;

	public string nextLevel;

	public string loadingScene;

	bool inLoadArea = false;

	public bool asyncLoad = true;

	void OnTriggerEnter2D(Collider2D other)
	{
		inLoadArea = true;
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "entry_point")
        {
			if (asyncLoad) {
				print("Loading " + nextLevel);
				SceneManager.LoadSceneAsync(nextLevel).allowSceneActivation = true;
			} else {
				//SceneManager.LoadSceneAsync(loadingScene);
			}
        }
        else if (other.tag == "exit_point")
        {
			inLoadArea = false;
			if (asyncLoad) {
				print("Loading " + nextLevel);
				SceneManager.LoadSceneAsync(nextLevel).allowSceneActivation = true;
				
			} else {
				//SceneManager.LoadScene(loadingScene);
			}
        }
    }

	void Start() {
	//		player.position = new Vector2 (entry_point.position.x + playerPos.x, entry_point.position.y + playerPos.y);
	}

	void Update() {
		if (inLoadArea) {
			playerPos = new Vector2 (Mathf.Abs(player.position.x) - Mathf.Abs(exit_point.position.x), Mathf.Abs(player.position.y) - Mathf.Abs(exit_point.position.y));
		}
	}

}