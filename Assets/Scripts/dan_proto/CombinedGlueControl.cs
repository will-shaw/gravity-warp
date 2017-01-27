using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedGlueControl : MonoBehaviour {

	public Transform gluePrefab;

	public int glueLimit;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			CombinedGravityWarp gwg = Camera.main.GetComponent<CombinedGravityWarp>();
			if(gwg.glueCount < glueLimit){
				Transform glueNew;
				glueNew = Instantiate(gluePrefab);
				Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouse.z = 0;
				glueNew.position = mouse;
				gwg.changeGlueCount(1);
				gwg.glues.Add(glueNew);
			}
		}
	}
}