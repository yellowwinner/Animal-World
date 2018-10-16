using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public GameObject ui;
	private Canvas uiCanvas;
	public float speed;

	private bool uiStatus = false;

	// Use this for initialization
	void Start () {
		uiCanvas = ui.GetComponent<Canvas>();
		uiCanvas.enabled = uiStatus;
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey("w")){
			transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime, Space.World);
		}
		if (Input.GetKey("s"))
        {
			transform.Translate(Vector3.back * speed * Time.fixedDeltaTime, Space.World);
        }
		if (Input.GetKey("d"))
        {
			transform.Translate(Vector3.right * speed * Time.fixedDeltaTime, Space.World);
        }
		if (Input.GetKey("a"))
        {
			transform.Translate(Vector3.left * speed * Time.fixedDeltaTime, Space.World);
        }

		//Show and hide the UI
		if (Input.GetKeyDown("h")){
			uiStatus = !uiStatus;
			uiCanvas.enabled = uiStatus;
		}
	}
}
