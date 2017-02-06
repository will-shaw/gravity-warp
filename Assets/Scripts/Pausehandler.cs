using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausehandler : MonoBehaviour {

    float timer =0f;
    bool active = false;
    public GameObject controls;

    public void showControls()
    {
        controls.SetActive(true);
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	
}
