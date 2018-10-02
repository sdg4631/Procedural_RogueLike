using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : MonoBehaviour 
{
	Animator myAnimator;
	Rigidbody2D myRigidBody;
	Raycast ray;

	bool isAlive = true;

	[SerializeField] int timeBetweenBounceMin = 1;
	[SerializeField] int timeBetweenBounceMax = 3;


	void Start() 
	{
		myAnimator = GetComponent<Animator>();
		myRigidBody = GetComponent<Rigidbody2D>();
		ray = GetComponent<Raycast>();
		StartCoroutine(StartBounceAnimation());
	}
	

	void Update() 
	{
		
	}

	IEnumerator StartBounceAnimation()
	{
		int timeBetweenBounce = Random.Range(timeBetweenBounceMin, timeBetweenBounceMax);

		while(isAlive)
		{
			yield return new WaitForSeconds(timeBetweenBounce);
			myAnimator.SetTrigger("Bounce");
		}
		
	}

	void Move()
	{
		var xVelocity = Random.Range(-3, 3);
		var yVelocity = Random.Range(-3, 3);
		
		if (ray.wallUp)
		{
			yVelocity = Random.Range(-4, -3);
		}
		if (ray.wallDown)
		{
			yVelocity = Random.Range(3, 4);
		}
		if (ray.wallLeft)
		{
			xVelocity = Random.Range(3, 4);
		}
		if (ray.wallRight)
		{
			xVelocity = Random.Range(-4, -3);
		}
		
        myRigidBody.velocity = new Vector2(xVelocity, yVelocity);
		
	}

	void Stop()
	{
		myRigidBody.velocity = new Vector2(0,0);
		myAnimator.ResetTrigger("Bounce");
	}
}
