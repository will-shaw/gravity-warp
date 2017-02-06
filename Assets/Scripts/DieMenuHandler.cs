
using UnityEngine;
using UnityEngine.SceneManagement;
public class DieMenuHandler : MonoBehaviour {
	public void ShowPause(){
		gameObject.SetActive(true);
	}
	public void Hide(){
		gameObject.SetActive(false);
	}
	public void reset(){
		Scene current = SceneManager.GetActiveScene();
		SceneManager.LoadScene(current.name);
		GravityWarp.gravity = "D";
	}
	public void mainMenu(){
		SceneManager.LoadScene("main_menu");
	}
   
}
