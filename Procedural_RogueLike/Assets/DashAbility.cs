// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DashAbility : MonoBehaviour 
// {
// 	// TODO remove public
// 	public DashState dashState;
//     public float dashTimer;
//     public float maxDash = 2f;
 
//     public Vector2 savedVelocity;
// 	Rigidbody2D myRigidBody;

// 	public enum DashState 
//  	{
//      Ready,
//      Dashing,
//      Cooldown
// 	}

// 	void Start()
// 	{
// 		myRigidBody = GetComponent<Rigidbody2D>();
// 	}
     
//     void Update () 
//     {
//         switch (dashState) 
//         {
// 			case DashState.Ready:
// 				var isDashKeyDown = Input.GetKeyDown (KeyCode.Space);
// 				if(isDashKeyDown)
// 				{
// 					savedVelocity = myRigidBody.velocity;
// 					myRigidBody.velocity =  new Vector2(myRigidBody.velocity.x * 50f, myRigidBody.velocity.y);
// 					dashState = DashState.Dashing;
// 				}
// 			break;

// 			case DashState.Dashing:
// 				dashTimer += Time.deltaTime;
// 				if(dashTimer >= maxDash)
// 				{
// 					dashTimer = maxDash;
// 					myRigidBody.velocity = savedVelocity;
// 					dashState = DashState.Cooldown;
// 				}
// 			break;

// 			case DashState.Cooldown:
// 				dashTimer -= Time.deltaTime;
// 				if(dashTimer <= 0)
// 				{
// 					dashTimer = 0;
// 					dashState = DashState.Ready;
// 				}
// 			break;
//         }
//     }
 
 
// }
