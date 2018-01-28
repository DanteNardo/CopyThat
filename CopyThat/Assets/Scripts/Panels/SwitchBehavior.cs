using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBehavior : Module {

	//public Camera camera;
	//public controlState currentState;

	// Use this for initialization
	void Start ()
	{
		 currentState = controlState.off; 
	}
 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Interact();
            }
        }
	}

	protected override void Interact()
	{
		if (currentState == controlState.on)
		{
			currentState = controlState.off;
		}
		else if (currentState == controlState.off)
		{
			currentState = controlState.on;
		}
		else
		{
			Debug.Log("ERROR: Switch state somehow set to " + currentState + ". Someone fucked up.");
		}
	}
}
