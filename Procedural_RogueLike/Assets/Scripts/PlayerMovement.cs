using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour 
{
	// Config
	[SerializeField] float horizontalMoveSpeed = 5;
	[SerializeField] float verticalMoveSpeed = 4;

	// Cached Component References
	Rigidbody2D myRigidBody;

    // Sprite Parent GameObjects
    [SerializeField] GameObject pitRoot;
	[SerializeField] GameObject pitForward;
	[SerializeField] GameObject pitForwardLR;
    [SerializeField] GameObject pitBack;
    [SerializeField] GameObject pitBackLR;

    //Meshes
	[SerializeField] GameObject pitForwardMeshes;
	[SerializeField] GameObject pitForwardLRMeshes;
    [SerializeField] GameObject pitBackMeshes;
    [SerializeField] GameObject pitBackLRMeshes;

    // Particles
    [SerializeField] GameObject fxParent;
    [SerializeField] GameObject pitFootstepFX;
    [SerializeField] GameObject dustTrailFX;

    // Cursor Variables
    bool aimingWithCursor = false;
    float cursorLastMovedTimer;
    Vector3 lastMouseCoordinate = Vector3.zero;

    // Controller Variables
    bool aimingWithController = false;

    bool playerIsMoving = false;
    Vector3 currentPos; 
    float footstepTimer = 0;
    float dustTimer = 0;


    // TODO remove public
	public DashState dashState;
    public float dashTimer;
    public float maxDash = 2f;
    public float dashSpeed = 5f;
 
    public Vector2 savedVelocity;

	public enum DashState 
 	{
     Ready,
     Dashing,
     Cooldown
	}

    
    

	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
        
        CheckForCursorMovement();
        CheckForPlayerMovement();
        Dash();
        if(dashState != DashState.Dashing) { Move(); }
        ControlSpriteWithCursorAiming();
        AimingWithController();
        PlayMovementParticles();
    }

    private void Dash()
    {
        switch (dashState) 
        {
			case DashState.Ready:
				var isDashKeyDown = Input.GetKeyDown(KeyCode.Space);
				if(isDashKeyDown)
				{
					savedVelocity = myRigidBody.velocity;
					myRigidBody.velocity =  new Vector2(myRigidBody.velocity.x * dashSpeed, myRigidBody.velocity.y * dashSpeed);
					dashState = DashState.Dashing;
				}
			break;

			case DashState.Dashing:
				dashTimer += Time.deltaTime;
				if(dashTimer >= maxDash)
				{
					dashTimer = maxDash;
					myRigidBody.velocity = savedVelocity;
					dashState = DashState.Cooldown;
				}
			break;

			case DashState.Cooldown:
				dashTimer -= Time.deltaTime;
				if(dashTimer <= 0)
				{
					dashTimer = 0;
					dashState = DashState.Ready;
				}
			break;
        }
    }

    private void CheckForPlayerMovement()
    {
        if (currentPos != transform.position)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
        currentPos = transform.position;
    }

    private void AimingWithController()
    {
        var rightAnalogXThrow = Input.GetAxis("Right Analog X");
        var rightAnalogYThrow = Input.GetAxis("Right Analog Y");

        if (rightAnalogXThrow == 0 && rightAnalogYThrow == 0)
        {
            aimingWithController = false;
        }
        else
        {
            aimingWithController = true;
        }

        bool aimingForwardWithController = rightAnalogXThrow >= -0.25 && rightAnalogXThrow <= 0.25 && rightAnalogYThrow < 0;
        bool aimingLeftWithController = rightAnalogXThrow < 0 && rightAnalogYThrow < 0.5;
        bool aimingRightWithController = rightAnalogXThrow > 0 && rightAnalogYThrow < 0.5;
        bool aimingBackWithController = rightAnalogXThrow >= -0.25 && rightAnalogXThrow <= 0.25 && rightAnalogYThrow > 0;
        bool aimingBackLeftWithController = rightAnalogXThrow < 0 && rightAnalogYThrow >= 0.5;
        bool aimingBackRightWithController = rightAnalogXThrow > 0 && rightAnalogYThrow >= 0.5;

        // Activate/Deactivate Parent GameObject
        if (aimingForwardWithController)
        {
            pitForwardMeshes.SetActive(true);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingLeftWithController || aimingRightWithController)
        {
            pitForwardLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingBackWithController)
        {
            pitBackMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingBackLeftWithController || aimingBackRightWithController)
        {
            pitBackLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
        }

        if (aimingLeftWithController || aimingBackLeftWithController)
        {
            pitForwardLR.transform.localScale = new Vector2(1, 1f);
            pitBackLR.transform.localScale = new Vector2(1, 1f);
        }


        if (aimingRightWithController || aimingBackRightWithController)
        {
            pitForwardLR.transform.localScale = new Vector2(-1, 1f);
            pitBackLR.transform.localScale = new Vector2(-1, 1f);
        }

        // Set Animation States
        if (pitForwardMeshes.activeInHierarchy)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLRMeshes.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBackMeshes.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLRMeshes.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
        }
    }

    void CheckForCursorMovement()
    {
        if (Input.mousePosition != lastMouseCoordinate || Input.GetMouseButtonDown(0)) // Moving Cursor or Attacking
        { 
            cursorLastMovedTimer = 0.0f;
            aimingWithCursor = true;
        }

        if (Input.mousePosition == lastMouseCoordinate) // Not Moving Cursor
        { 
            cursorLastMovedTimer += Time.deltaTime;
        }

        if (cursorLastMovedTimer >= .5f)
        {
            aimingWithCursor = false;
        }
        lastMouseCoordinate = Input.mousePosition;
    }

    void Move()
    {
        // Player Input Returns
        var xThrow = Input.GetAxis("Horizontal");
        var yThrow = Input.GetAxis("Vertical");

        // X Movement 
        var xVelocity = new Vector2(xThrow * horizontalMoveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = xVelocity;

        // Y Movement 
        var yVelocity = new Vector2(myRigidBody.velocity.x, yThrow * verticalMoveSpeed);
        myRigidBody.velocity = yVelocity;

          
        bool playerNotMovingWithInput = xThrow == 0 && yThrow == 0;
        if (playerNotMovingWithInput || !playerIsMoving)
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", false);
        }
        else
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", true);
        }
        currentPos = transform.position;  

        if (aimingWithCursor == false && aimingWithController == false)
        {
            ControlSpriteWithMoving(xThrow, yThrow);
		    FlipSpriteWhileMoving();
        }
    }

    void ControlSpriteWithMoving(float xThrow, float yThrow)
    {    
        bool playerMovingForward = xThrow >= -0.25 && xThrow <= 0.25 && yThrow < 0;
        bool playerMovingLeft = xThrow < 0 && yThrow < 0.5;
        bool playerMovingRight = xThrow > 0 && yThrow < 0.5;
        bool playerMovingBack = xThrow >= -0.25 && xThrow <= 0.25 && yThrow > 0;
        bool playerMovingBackLeft = xThrow < 0 && yThrow >= 0.5;
        bool playerMovingBackRight = xThrow > 0 && yThrow >= 0.5;

        // Activate/Deactivate Parent GameObject
        if (playerMovingForward)
        {
            pitForwardMeshes.SetActive(true);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
            
        }
        else if (playerMovingLeft || playerMovingRight)
        {
            pitForwardLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (playerMovingBack)
        {
            pitBackMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (playerMovingBackLeft || playerMovingBackRight)
        {
            pitBackLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
        }

        // Set Animation States
        if (pitForwardMeshes.activeInHierarchy)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLRMeshes.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBackMeshes.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLRMeshes.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
        }
    }

    void FlipSpriteWhileMoving()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			pitForwardLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
            pitBackLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
		}
	}

    void ControlSpriteWithCursorAiming()
    {
        Vector3 cursor = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        cursor = Camera.main.ScreenToWorldPoint(cursor);

        float angle = Mathf.Atan2(cursor.y - transform.position.y, cursor.x - transform.position.x) * Mathf.Rad2Deg;

        var facingForward = angle >= -135 && angle <= -45;
        var facingForwardLR = ((angle < -135 && angle > -180) || (angle < 180 && angle > 145)) || ((angle < 0 && angle > -45) || (angle >= 0 && angle < 35));
        var facingBack = angle <= 105 && angle >= 75;
        var facingBackLR = (angle <= 135 && angle > 105) || (angle < 75 && angle >= 45);

        if (aimingWithCursor == true) // TODO or firing???
        {
            // Activate/Deactivate Parent GameObject
            if (facingForward)
            {
                pitForwardMeshes.SetActive(true);
                pitForwardLRMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);

            }
            else if (facingForwardLR)
            {
                pitForwardLRMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);
            }
            else if (facingBack)
            {
                pitBackMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitForwardLRMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);
            }
            else if (facingBackLR)
            {
                pitBackLRMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitForwardLRMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
            }

            // Set Animation States
            if (pitForwardMeshes.activeInHierarchy)
            {
                pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
            }
            else if (pitForwardLRMeshes.activeInHierarchy)
            {
                pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
            }
            else if (pitBackMeshes.activeInHierarchy)
            {
                pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
            }
            else if (pitBackLRMeshes.activeInHierarchy)
            {
                pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
            }

            FlipSpriteWhileAiming(angle);
        } 
    }
        
    void FlipSpriteWhileAiming(float angle)
    {
        var spriteFacingLeft = ((angle < -135 && angle > -180) || (angle < 180 && angle > 145)) || (angle <= 135 && angle > 95);
        var spriteFacingRight = ((angle < 0 && angle > -45) || (angle >= 0 && angle < 35)) || (angle < 85 && angle >= 45);

        if (spriteFacingLeft)
        {
            pitForwardLR.transform.localScale = new Vector2(1, 1f);
            pitBackLR.transform.localScale = new Vector2(1, 1f);
        }


        if (spriteFacingRight)
        {
            pitForwardLR.transform.localScale = new Vector2(-1, 1f);
            pitBackLR.transform.localScale = new Vector2(-1, 1f);
        }
    }

    void PlayMovementParticles()
    {
        // Footsteps
        if (playerIsMoving && footstepTimer <= 0)
        {   
            var footstepSpawnPosition = new Vector2(transform.position.x, transform.position.y - .5f);       
            var footsteps = Instantiate(pitFootstepFX, footstepSpawnPosition, Quaternion.identity);
            footsteps.transform.parent = fxParent.transform;
            Destroy(footsteps, 1f);
            footstepTimer = .2f;           
        }
        footstepTimer -= Time.deltaTime;

        // Dust Trail
        if (playerIsMoving && dustTimer <= 0)
        {
            var dustTrailSpawnPosition = new Vector2(transform.position.x, transform.position.y - .5f);
            var dustTrail = Instantiate(dustTrailFX, dustTrailSpawnPosition, Quaternion.identity);
            dustTrail.transform.parent = fxParent.transform;
            float destroyTimer = 3f;
            Destroy(dustTrail, destroyTimer);
            dustTimer = 0.02f;
        }
        dustTimer -= Time.deltaTime;
    }

}
