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

	[SerializeField] GameObject pitForward;
	[SerializeField] GameObject pitForwardLR;
    [SerializeField] GameObject pitBack;
    [SerializeField] GameObject pitBackLR;

    // Cursor Variables
    bool aimingWithCursor = false;
    float cursorLastMovedTimer;
    Vector3 lastMouseCoordinate = Vector3.zero;
    

	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update()
    {
        CheckForCursorMovement();
        Move();
        ControlSpriteWithAiming();
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

        if (aimingWithCursor == false)
        {
            ControlSpriteWithMoving(xThrow, yThrow);
		    FlipSpriteWhileMoving();
        }
        

    }

    void ControlSpriteWithMoving(float xThrow, float yThrow)
    {    
        bool playerNotMoving = xThrow == 0 && yThrow == 0;
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

        // // Set Animation States
        // if (pitForward.activeInHierarchy == true)
        // {
        //     pitForward.GetComponent<Animator>().SetBool("RunForward", playerMovingForward);
        // }
        // else if (pitForwardLR.activeInHierarchy)
        // {
        //     pitForwardLR.GetComponent<Animator>().SetBool("RunLR", playerMovingLeft || playerMovingRight);
        // }
        // else if (pitBack.activeInHierarchy)
        // {
        //     pitBack.GetComponent<Animator>().SetBool("RunBack", playerMovingBack);
        // }
        // else if (pitBackLR.activeInHierarchy)
        // {
        //     pitBackLR.GetComponent<Animator>().SetBool("RunBackLR", playerMovingBackLeft || playerMovingBackRight);
        // }
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

    void ControlSpriteWithAiming()
    {
        if (aimingWithCursor == true) // TODO or firing???
        {
           Vector3 cursor = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            cursor = Camera.main.ScreenToWorldPoint(cursor);
            cursor = cursor - transform.position;

            

            float angle = Mathf.Atan2(cursor.y - transform.position.y, cursor.x - transform.position.x) * Mathf.Rad2Deg;

            var facingForward = angle >= -135 && angle <= -45;
            var facingForwardLR = ((angle < -135 && angle > -180) || (angle < 180 && angle > 145)) || ((angle < 0 && angle > -45) || (angle >= 0 && angle < 35));
            var facingBack = angle <= 105 && angle >= 75;
            var facingBackLR = (angle <= 135 && angle > 105) || (angle < 75 && angle >= 45);

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


}
