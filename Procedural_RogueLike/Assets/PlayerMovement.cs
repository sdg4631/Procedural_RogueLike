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

    // Sprite GameObjects
    [SerializeField] GameObject pitRoot;
	[SerializeField] GameObject pitForward;
	[SerializeField] GameObject pitForwardLR;
    [SerializeField] GameObject pitBack;
    [SerializeField] GameObject pitBackLR;

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
    float footstepTimer = 0;
    float dustTimer = 0;
    

	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
        CheckForCursorMovement();
        CheckForPlayerMovement();
        Move();
        ControlSpriteWithCursorAiming();
        AimingWithController();
        PlayMovementParticles();
    }



    private void CheckForPlayerMovement()
    {
        if (myRigidBody.velocity.magnitude > 0)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
    }

    private void AimingWithController()
    {
        var rightAnologXThrow = Input.GetAxis("Right Analog X");
        var rightAnologYThrow = Input.GetAxis("Right Analog Y");

        if (rightAnologXThrow == 0 && rightAnologYThrow == 0)
        {
            aimingWithController = false;
        }
        else
        {
            aimingWithController = true;
        }

        bool aimingForwardWithController = rightAnologXThrow >= -0.25 && rightAnologXThrow <= 0.25 && rightAnologYThrow < 0;
        bool aimingLeftWithController = rightAnologXThrow < 0 && rightAnologYThrow < 0.5;
        bool aimingRightWithController = rightAnologXThrow > 0 && rightAnologYThrow < 0.5;
        bool aimingBackWithController = rightAnologXThrow >= -0.25 && rightAnologXThrow <= 0.25 && rightAnologYThrow > 0;
        bool aimingBackLeftWithController = rightAnologXThrow < 0 && rightAnologYThrow >= 0.5;
        bool aimingBackRightWithController = rightAnologXThrow > 0 && rightAnologYThrow >= 0.5;

        // Activate/Deactivate Parent GameObject
        if (aimingForwardWithController)
        {
            pitForward.SetActive(true);
            pitForwardLR.SetActive(false);
            pitBack.SetActive(false);
            pitBackLR.SetActive(false);

        }
        else if (aimingLeftWithController || aimingRightWithController)
        {
            pitForwardLR.SetActive(true);
            pitForward.SetActive(false);
            pitBack.SetActive(false);
            pitBackLR.SetActive(false);
        }
        else if (aimingBackWithController)
        {
            pitBack.SetActive(true);
            pitForward.SetActive(false);
            pitForwardLR.SetActive(false);
            pitBackLR.SetActive(false);
        }
        else if (aimingBackLeftWithController || aimingBackRightWithController)
        {
            pitBackLR.SetActive(true);
            pitForward.SetActive(false);
            pitForwardLR.SetActive(false);
            pitBack.SetActive(false);
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
        if (pitForward.activeInHierarchy == true)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLR.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBack.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLR.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
        }
    }

    void CheckForCursorMovement()
    {
        if (Input.mousePosition != lastMouseCoordinate) // Moving Cursor
        { 
            cursorLastMovedTimer = 0.0f;
            aimingWithCursor = true;
        }

        if (Input.mousePosition == lastMouseCoordinate) // Not Moving Cursor
        { 
            cursorLastMovedTimer += Time.deltaTime;
        }

        if (cursorLastMovedTimer >= 1f)
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

        
        bool playerNotMoving = xThrow == 0 && yThrow == 0;
        if (playerNotMoving)
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", false);
        }
        else
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", true);
        }

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
            pitForward.SetActive(true);
            pitForwardLR.SetActive(false);
            pitBack.SetActive(false);
            pitBackLR.SetActive(false);
            
        }
        else if (playerMovingLeft || playerMovingRight)
        {
            pitForwardLR.SetActive(true);
            pitForward.SetActive(false);
            pitBack.SetActive(false);
            pitBackLR.SetActive(false);
        }
        else if (playerMovingBack)
        {
            pitBack.SetActive(true);
            pitForward.SetActive(false);
            pitForwardLR.SetActive(false);
            pitBackLR.SetActive(false);
        }
        else if (playerMovingBackLeft || playerMovingBackRight)
        {
            pitBackLR.SetActive(true);
            pitForward.SetActive(false);
            pitForwardLR.SetActive(false);
            pitBack.SetActive(false);
        }

        // Set Animation States
        if (pitForward.activeInHierarchy == true)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLR.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBack.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLR.activeInHierarchy)
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
        cursor = cursor - transform.position;

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
                pitForward.SetActive(true);
                pitForwardLR.SetActive(false);
                pitBack.SetActive(false);
                pitBackLR.SetActive(false);

            }
            else if (facingForwardLR)
            {
                pitForwardLR.SetActive(true);
                pitForward.SetActive(false);
                pitBack.SetActive(false);
                pitBackLR.SetActive(false);
            }
            else if (facingBack)
            {
                pitBack.SetActive(true);
                pitForward.SetActive(false);
                pitForwardLR.SetActive(false);
                pitBackLR.SetActive(false);
            }
            else if (facingBackLR)
            {
                pitBackLR.SetActive(true);
                pitForward.SetActive(false);
                pitForwardLR.SetActive(false);
                pitBack.SetActive(false);
            }

            // Set Animation States
            if (pitForward.activeInHierarchy == true)
            {
                pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
            }
            else if (pitForwardLR.activeInHierarchy)
            {
                pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
            }
            else if (pitBack.activeInHierarchy)
            {
                pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
            }
            else if (pitBackLR.activeInHierarchy)
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
