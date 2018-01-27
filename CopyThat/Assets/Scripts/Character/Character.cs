using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    enum MOVEMENT_STATE
    {
        Idle, 
        Moving, 
        Jumping
    }

    // Member Attributes 

    public float SPEED_X = 10.0f;
    public float jumpForce = 1000.0f;
    public float maxVelY = 1000.0f;

    MOVEMENT_STATE movementState;

    bool movingRight;
    bool movingLeft;
    bool jumping;

    Rigidbody2D rb2d;
    Vector2 velocity;
    Collider2D collider; 

    float isGroundedRayLength = 0.1f;
    public LayerMask layerMaskForGrounded;
    public bool isGrounded
    {
        get
        {
            Vector3 position = transform.position;
            position.y = collider.bounds.min.y + 0.1f;
            float length = isGroundedRayLength + 0.1f;
            Debug.DrawRay(position, Vector3.down * length, Color.red);
            bool grounded = Physics2D.Raycast(position, Vector3.down, length, layerMaskForGrounded.value);
            return grounded;
        }
    }

    // Use this for initialization
    void Start () {
        movingLeft = false;
        movingRight = false;
        jumping = false;
        velocity = Vector2.zero; 
        movementState = MOVEMENT_STATE.Idle;
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>(); 
	}
	
	// Update is called once per frame
	void Update () {

        // Draw ray in front of character for debugging 
        Debug.DrawLine(transform.position, Vector3.right * 10.0f, Color.green);

        UpdateInput();
    }

    private void FixedUpdate()
    {
        velocity.x *= SPEED_X;

        rb2d.AddForce(velocity);
        velocity.x = 0; 
    }

    void UpdateInput()
    {

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (moveHorizontal > 0)
        {
            //Debug.Log("Moving Right"); 
            movingRight = true;
            movementState = MOVEMENT_STATE.Moving; 
        }
        else if (moveHorizontal < 0)
        {
            //Debug.Log("Moving Left"); 
            movingLeft = true;
            movementState = MOVEMENT_STATE.Moving;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            movementState = MOVEMENT_STATE.Jumping;
            jumping = true;

            rb2d.AddForce(new Vector2(0, 100));
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce); 
            //Debug.Log("Jump"); 
        }

        

        //if (moveHorizontal == 0 && isGrounded)
        //{
        //    movementState = MOVEMENT_STATE.Idle;
        //    movingLeft = false;
        //    movingRight = false;
        //    jumping = false; 
        //}


        velocity.x += moveHorizontal;
       
        //velocity.y = Mathf.Clamp(velocity.y, 0 , maxVelY);
    }

   
}
