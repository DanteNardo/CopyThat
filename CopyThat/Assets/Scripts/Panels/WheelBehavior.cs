using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelBehavior : Module
{
    public float minGoalAngle;
    public float maxGoalAngle;
    float initialClickAngle;
    float initialWheelClickAngle;

    /// <summary>
    /// This does nothing
    /// </summary>
    protected override void Interact()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        //bool startAboveMax = Random.Range(0, 2) == 1;   // If true, start above maximum angle. If false, start below minimum angle.
    }


    //Update is called once per frame
    void Update()
    {
        Debug.Log("BNJKAB" + GetComponent<RectTransform>().rotation.eulerAngles.z);

        if ((maxGoalAngle >= minGoalAngle
                && GetComponent<RectTransform>().rotation.eulerAngles.z <= maxGoalAngle
                && GetComponent<RectTransform>().rotation.eulerAngles.z >= minGoalAngle)
            || (maxGoalAngle < minGoalAngle
                && GetComponent<RectTransform>().rotation.eulerAngles.z >= maxGoalAngle
                && GetComponent<RectTransform>().rotation.eulerAngles.z <= minGoalAngle))
        {
            currentState = controlState.on;
        }
        else
        {
            //currentState = controlState.off;
        }
    }

    private void OnMouseDown()
    {
        Debug.DrawRay(transform.position, Input.mousePosition - transform.position);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position - Input.mousePosition);
        initialClickAngle = Vector2.SignedAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        initialWheelClickAngle = transform.rotation.eulerAngles.z;
    }
    
    private void OnMouseDrag()
    {
        transform.rotation = Quaternion.Euler(0, 0,
            Vector2.SignedAngle(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)
                - initialClickAngle + initialWheelClickAngle);

    }

    /// <summary>
    /// Determine if the goal for this wheel has been met
    /// </summary>
    /// <returns>True if within the angle range</returns>
    public bool IsGoalReached()
    {
        return currentState == controlState.on;
    }
}
