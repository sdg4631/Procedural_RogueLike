using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour 
{
	Rigidbody2D myRigidBody;

	[SerializeField] float horizontalMoveSpeed = 5;
	[SerializeField] float verticalMoveSpeed = 4;

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
		var xThrow = Input.GetAxis("Horizontal");
		var xVelocity = new Vector2(xThrow * horizontalMoveSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = xVelocity;
		
		var yThrow = Input.GetAxis("Vertical");
		var yVelocity = new Vector2(myRigidBody.velocity.x, yThrow * verticalMoveSpeed);
		myRigidBody.velocity = yVelocity;
	
	}
}
