
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
		if(Camera.main.GetComponent<GravityWarp>().playerDead){
			Info.load = true;
		}
		else{
			Info.load = false;
		}
		Scene current = SceneManager.GetActiveScene();
		SceneManager.LoadScene(current.name);
		GravityWarp.gravity = "D";
	}
	public void mainMenu(){
		SceneManager.LoadScene("main_menu");
	}
   
}
