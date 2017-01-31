using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueControl : MonoBehaviour {

	public Transform gluePrefab;

	public int glueLimit;

	public int glueCount;


	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			GravityWarp gw = Camera.main.GetComponent<GravityWarp>();
			if(glueCount < glueLimit){
				Transform glueNew;
				glueNew = Instantiate(gluePrefab);
				Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouse.z = 0;
				glueNew.position = mouse;
				glueCount++;
				gw.glues.Add(glueNew);
			}
		}
	}

	public void changeGlueCount(int i) {
		if(i == 0) {
			glueCount--;
		} else {
			glueCount++;
		}
	}
}
