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
	


	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	void Update() 
	{
		Move();		
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

        AnimateSprite(xThrow, yThrow);
		FlipSprite();

    }

    void AnimateSprite(float xThrow, float yThrow)
    {
        // Set Animation States
        bool playerNotMoving = xThrow == 0 && yThrow == 0;
        bool playerMovingForward = xThrow == 0 && yThrow < 0;
        bool playerMovingLeft = xThrow < 0 && yThrow <= 0;
        bool playerMovingRight = xThrow > 0 && yThrow <= 0;
        bool playerMovingBack = xThrow == 0 && yThrow > 0;
        bool playerMovingBackLeft = xThrow < 0 && yThrow > 0;
        bool playerMovingBackRight = xThrow > 0 && yThrow > 0;

        if (playerMovingForward)
            print("playerMovingForward");

        if (playerMovingLeft)
            print("playerMovingLeft");

        if (playerMovingRight)
            print("playerMovingRight");

        if (playerNotMoving)
            print("playerNotMoving");

        pitForwardLR.SetActive(playerMovingLeft || playerMovingRight);
        pitForward.SetActive(playerNotMoving || playerMovingForward);
        pitForward.GetComponent<Animator>().SetBool("RunForward", playerMovingForward);
    }

    void FlipSprite()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			pitForwardLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
		}
	}
}
