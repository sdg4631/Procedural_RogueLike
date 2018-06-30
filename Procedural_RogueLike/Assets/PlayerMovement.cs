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

        AnimateSprite();
		// FlipSprite();

    }

    void AnimateSprite()
    {    
        // Player Input Returns   
        var xThrowAim = Input.GetAxis("HorizontalStick");
        var yThrowAim = Input.GetAxis("VerticalStick");

        bool playerNotMoving = xThrowAim == 0 && yThrowAim == 0;
        bool playerMovingForward = xThrowAim >= -0.25 && xThrowAim <= 0.25 && yThrowAim < 0;
        bool playerMovingLeft = xThrowAim < 0 && yThrowAim <= 0;
        bool playerMovingRight = xThrowAim > 0 && yThrowAim <= 0;
        bool playerMovingBack = xThrowAim >= -0.5 && xThrowAim <= 0.5 && yThrowAim > 0;
        bool playerMovingBackLeft = xThrowAim < 0 && yThrowAim > 0;
        bool playerMovingBackRight = xThrowAim > 0 && yThrowAim > 0;

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
            pitForward.GetComponent<Animator>().SetBool("RunForward", playerMovingForward);
        }
        else if (pitForwardLR.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("RunLR", playerMovingLeft || playerMovingRight);
        }
        else if (pitBack.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("RunBack", playerMovingBack);
        }
        else if (pitBackLR.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("RunBackLR", playerMovingBackLeft || playerMovingBackRight);
        }

        bool playerHasHorizontalSpeed = Mathf.Abs(xThrowAim) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			pitForwardLR.transform.localScale = new Vector2(-Mathf.Sign(xThrowAim), 1f);
            pitBackLR.transform.localScale = new Vector2(Mathf.Sign(xThrowAim), 1f);
		}
    }

    // void FlipSprite()
	// {
	// 	bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
	// 	if (playerHasHorizontalSpeed)
	// 	{
	// 		pitForwardLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
    //         pitBackLR.transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
	// 	}
	// }
}
