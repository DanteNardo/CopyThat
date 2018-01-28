using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(InputManager))]
public class Player : MonoBehaviour
{

    // Public Attributes 

    public PanelManager panelManager; 

    public float maxJumpHeight = 3.5f;
    public float minJumpHeight = 1; 
    public float timeToJumpApex = 0.4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
	public int wallJumpLimit = 3;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallSlideSpeedMax = 3.0f;
    public float wallStickTime = 0.25f;
    public float timeToWallUnstick;

    [HideInInspector]
    public Vector3 velocity;

    public LayerMask panelCollisionMask; 

    // Private Attributes 

	// Movement / Physics 
    private Controller2D controller;
    private InputManager input; 
    private Animator anim;            // Reference to the player's animator component.
    private bool facingRight;         // Boolean for direction 
    private float velocityXSmoothing;
    private float gravity;
    private float maxJumpForce;
    private float minJumpForce;
    private Vector2 updatedInput;
    private bool wallSliding;
    private int wallDirX;
	private int wallJumpNum;
	private bool canWallJump;		// boolean for ability to wallJump so limit is possible
	// Rendering
	private SpriteRenderer spriteReference;
	private Color defaultColor;

	// Gameplay

    private void Awake()
    {
        facingRight = true;
		canWallJump = true;
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        input = GetComponent<InputManager>(); 
		spriteReference = GetComponent<SpriteRenderer> (); 
		defaultColor = spriteReference.color; 
    }

    // Use this for initialization
    private void Start()
    {

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpForce = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpForce = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        //print("Gravity:" + gravity + " Jump Velocity: " + jumpForce);
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO check if game is won or lost 

        if(GameStateManager.Instance.GameState != GAME_STATE.Navigating)
        {
            return; 
        }

        if(input.GetSubmitDown())
        {
            //Debug.Log("Submit Pressed"); 
        }
        

        if(controller.collisions.below)
        {
			canWallJump = true;
        }


        updatedInput = input.GetAxes(); //new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        CalculateVelocity();
        //HandleWallSliding();

        // Jump Input 
        if (input.GetJumpingDown())
        {
            OnJumpInputDown(); 
        }
		else
		{
			OnJumpInputUp();
		}

        controller.Move(velocity * Time.deltaTime, updatedInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            // Sliding down slope, scale by gravity and angle of slope
            if(controller.collisions.slidingDownMaxSlope)
            {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime; 
            }
            else
            {
                // On the floor, no y velocity
                velocity.y = 0;
            }
        }

        //Debug.Log(velocity); 
        //Debug.Log(controller.collisions.slopeAngle); 
        //Debug.Log("climbing: " + controller.collisions.climbingSlope); 
    }

    private void HandleWallSliding()
    {
        // Wall sliding 
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (updatedInput.x != wallDirX && updatedInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }

        }
    }

    private void CalculateVelocity()
    {
        // Set X velocity first 
        float targetVelocityX = updatedInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }

    private void OnJumpInputDown()
    {

        // Wall jumping 
		if (wallSliding && canWallJump)
        {
			if(wallJumpNum < wallJumpLimit)
			{
				wallJumpNum += 1;

				if (wallDirX == updatedInput.x)
	            {
	                velocity.x = -wallDirX * wallJumpClimb.x;
	                velocity.y = wallJumpClimb.y;
	            }
	            else if (updatedInput.x == 0)
	            {
	                velocity.x = -wallDirX * wallJumpOff.x;
	                velocity.y = wallJumpOff.y;
	            }
	            else
	            {
	                velocity.x = -wallDirX * wallLeap.x;
	                velocity.y = wallLeap.y;
	            }
			}
			else
			{
				wallJumpNum = 0;

				if(controller.collisions.below && velocity.y == 0)
				{
					wallJumpNum = 0;
					canWallJump = true;
				}

				canWallJump = false;
			}
        }
        if (controller.collisions.below)
        {
			wallJumpNum = 0;
            if (controller.collisions.slidingDownMaxSlope)
            {
                if (updatedInput.x != -Mathf.Sign(controller.collisions.slopeNormal.x))
                { // not jumping against max slope
                    velocity.y = maxJumpForce * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpForce * controller.collisions.slopeNormal.x;
                }
            }
            else
            {
                velocity.y = maxJumpForce;
            }
        }
    }

    private void OnJumpInputUp()
    {
        if (velocity.y > minJumpForce)
        {
            velocity.y = minJumpForce;
        }
    }

    private void FixedUpdate()
    {
        #region Animation Settings 
        if (controller.collisions.below)
        {
            anim.SetBool("Ground", true);
        }
        else
        {
            anim.SetBool("Ground", false);
        }

        
        // Set the vertical animation
        anim.SetFloat("vSpeed", velocity.y);
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("Speed", Mathf.Abs(velocity.x));
        

        // If the input is moving the player right and the player is facing left...
		if (velocity.x > 0 && !facingRight && !controller.collisions.slidingDownMaxSlope)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
		else if (velocity.x < 0 && facingRight && !controller.collisions.slidingDownMaxSlope)
        {
            // ... flip the player.
            Flip();
        }
        #endregion
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        //theScale.x *= -1;
        //transform.localScale = theScale;
        spriteReference.flipX = !spriteReference.flipX; 
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BridgePanelController panel = collision.gameObject.GetComponent<BridgePanelController>(); 
        if(panel != null)
        {
            if(Input.GetButtonDown("Submit"))
            {
                switch(panel.bridgePanelType)
                {
                    case BridgePanelController.PANEL_TYPE.COMMS:
                        panelManager.OpenComs();
                        break; 
                    case BridgePanelController.PANEL_TYPE.ENG:
                        panelManager.OpenEng(); 
                        break;
                    case BridgePanelController.PANEL_TYPE.FLIGHT:
                        panelManager.OpenFlight(); 
                        break;
                    case BridgePanelController.PANEL_TYPE.SECURITY:
                        panelManager.OpenSecurity(); 
                        break;
                }
            }

            if (Input.GetButtonDown("Cancel"))
            {
                switch (panel.bridgePanelType)
                {
                    case BridgePanelController.PANEL_TYPE.COMMS:
                        panelManager.CloseComs();
                        break;
                    case BridgePanelController.PANEL_TYPE.ENG:
                        panelManager.CloseEng();
                        break;
                    case BridgePanelController.PANEL_TYPE.FLIGHT:
                        panelManager.CloseFlight();
                        break;
                    case BridgePanelController.PANEL_TYPE.SECURITY:
                        panelManager.CloseSecurity();
                        break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}

