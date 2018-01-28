using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCrossLogic : MonoBehaviour {

    public bool xLock;
    public bool yLock;
    public GameObject cursor;
    RectTransform myPos;
    RectTransform cursorPos;
	// Use this for initialization
	void Start ()
    {
        myPos = gameObject.GetComponent<RectTransform>();
        cursorPos = cursor.GetComponent<RectTransform>();
       
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (xLock)
        {
            myPos.localPosition = new Vector2(myPos.localPosition.x, cursorPos.localPosition.y);
        }
        else if (yLock)
        {
            myPos.localPosition = new Vector2(cursorPos.localPosition.x, myPos.localPosition.y);
        }
        else
        {
            Debug.Log("Please select an axis lock");
        }
    }
}
