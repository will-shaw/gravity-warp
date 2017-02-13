using UnityEngine;

public class Checkpoint : MonoBehaviour {


	public GameObject checkpointTxt;

	public Vector3 respawnPosistion;

	bool once = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag ==  "Player" && !(once))
		{
			checkpointTxt.SetActive(true);
			Info.checkpoint = respawnPosistion;
		}
	}
}
