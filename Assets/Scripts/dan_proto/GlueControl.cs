using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueControl : MonoBehaviour {

	public Transform gluePrefab;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			GravityWarpGlue gwg = Camera.main.GetComponent<GravityWarpGlue>();
			if(gwg.glueCount < gwg.glue.Length){
				Transform glueNew;
				glueNew = Instantiate(gluePrefab);
				Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouse.z = 0;
				glueNew.position = mouse;
				gwg.changeGlueCount(1);
				int currentGlueCount = gwg.getGlueCount();
				gwg.glue[currentGlueCount-1] = glueNew;
			}
		}
	}
}
