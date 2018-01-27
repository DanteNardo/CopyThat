using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawSliderBehavior : Module
{
   // public controlState currentState;
    private float min;
    private float middle;
    private float max;
    public GameObject MinMark;
    public GameObject MaxMark;
    public GameObject arrow;
    float arPos;
    Vector3 screenPoint;
    Vector3 offset;
    public float testReturn;

    // Use this for initialization
    void Start()
    {
        min = MinMark.transform.position.y;
        max = MaxMark.transform.position.y;
    
        transform.position = new Vector3(gameObject.transform.position.x, Random.Range(min, max), gameObject.transform.position.z);

    }


    // Update is called once per frame
    void Update()
    {
        arPos = arrow.transform.position.y;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Interact();
            }
        }

       


        // Stops transformation if too far
        if (arPos <= min)
        {
            transform.position = new Vector3(gameObject.transform.position.x, min, gameObject.transform.position.z);
        }
        else if (arPos >= max)
        {
            transform.position = new Vector3(gameObject.transform.position.x, max, gameObject.transform.position.z);
        }

        // Checks where arrow is and sets state
        testReturn = RawSliderValue();

    }

    // Moves the slider
    protected override void Interact()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
       // Debug.Log("Inteact Hit");
       
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint - offset);
        transform.position = new Vector3(gameObject.transform.position.x, curPosition.y, gameObject.transform.position.z);
    }

    public float RawSliderValue()
    {
        float returnValue;

        returnValue = (arrow.transform.position.y - min) / (max - min);

        return (returnValue * 2) -1;
    }


}
