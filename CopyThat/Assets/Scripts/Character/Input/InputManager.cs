using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    /// <summary>
    /// This Class functions as an interface manager between all input values and classes that access these values. Member values are private and contain getters for the values. 
    /// </summary>

    private Vector2 axes;
    private bool isJumpingDown;
    private bool isSubmitDown;
    private bool isDownPressed;
    private bool isUpPressed; 


    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        // Get input axes from controller 
        axes = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        isDownPressed = (Input.GetAxis("Vertical") < 0) ? true : false;
        isUpPressed = (Input.GetAxis("Vertical") > 0) ? true : false;

        // Get button presses from controller
        isJumpingDown = Input.GetButton("Jump");
        isSubmitDown = Input.GetButton("Submit");
    }

    // Class Getters ---------------------------------------------

    public Vector2 GetAxes()
    {
        return axes;
    }

    public bool GetJumpingDown()
    {
        return isJumpingDown;
    }

    public bool GetSubmitDown()
    {
        return isSubmitDown; 
    }

    public bool GetDownPressed()
    {
        return isDownPressed; 
    }

    public bool GetUpPressed()
    {
        return isUpPressed; 
    }

}
