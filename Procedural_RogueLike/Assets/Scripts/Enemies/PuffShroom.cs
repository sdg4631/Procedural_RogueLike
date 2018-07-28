using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffShroom : MonoBehaviour 
{
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	Raycast ray;

	[SerializeField] GameObject puffFXPrefab;
	[SerializeField] GameObject fxParent; 
	[SerializeField] Transform target;
	[SerializeField] int timeBetweenPuffsMin = 3;
	[SerializeField] int timeBetweenPuffsMax = 9;

	private bool isAlive = true;


	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
		ray = GetComponent<Raycast>();
		StartCoroutine(StartPuffAnimation());	
	}

	void Update()
	{
		
	}

    void PuffParticles()
    {
		myAnimator.ResetTrigger("Puff");
        var puffFX = Instantiate(puffFXPrefab, transform.position, Quaternion.identity);
		puffFX.transform.parent = fxParent.transform;
		float destroyDelay = 4f;
        Destroy(puffFX, destroyDelay);
    }

    void Move()
    {

		var xVelocity = Random.Range(-12, 12);
		var yVelocity = Random.Range(-12, 12);
		
		if (ray.wallUp)
		{
			yVelocity = Random.Range(-8, -12);
		}
		if (ray.wallDown)
		{
			yVelocity = Random.Range(8, 12);
		}
		if (ray.wallLeft)
		{
			xVelocity = Random.Range(8, 12);
		}
		if (ray.wallRight)
		{
			xVelocity = Random.Range(-8, -12);
		}
		
        myRigidBody.velocity = new Vector2(xVelocity, yVelocity);

		
		// To be called in Update for continuous chasing movement
		// transform.position += (player.transform.position - transform.position).normalized * MoveSpeed * Time.deltaTime;
    }

	void Stop()
	{
		myRigidBody.velocity = new Vector2(0,0);
	}

	IEnumerator StartPuffAnimation()
	{
		int timeBetweenPuffs = Random.Range(timeBetweenPuffsMin, timeBetweenPuffsMax);

		while(isAlive)
		{
			yield return new WaitForSeconds(timeBetweenPuffs);
			myAnimator.SetTrigger("Puff");
		}
		
	}	
}
