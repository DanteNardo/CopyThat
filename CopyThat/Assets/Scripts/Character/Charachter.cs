using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charachter : MonoBehaviour {

    // Member Attributes 
    bool movingRight;
    bool movingLeft; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Draw ray in front of character for debugging 
        Debug.DrawLine(transform.position, Vector3.right * 3.0f, Color.green); 
    }

    void UpdateInput()
    {
        if ((Input.GetKeyDown(KeyCode.D))) 
        {
            movingRight = true; 
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            movingLeft = true; 
        }

    }
}
