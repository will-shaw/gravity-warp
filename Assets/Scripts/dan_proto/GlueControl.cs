using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueControl : MonoBehaviour {

	public Transform gluePrefab;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			Transform glue;
			glue = Instantiate(gluePrefab);
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mouse.z = 0;
			glue.position = mouse;
		}
	}
}
