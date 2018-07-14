using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffShroom : MonoBehaviour 
{
	Rigidbody2D myRigidBody;
	Animator myAnimator;
	PlayerMovement player;

	[SerializeField] GameObject puffFXPrefab;
	[SerializeField] GameObject fxParent; 
	[SerializeField] Transform target;
	[SerializeField] int timeBetweenPuffsMin = 3;
	[SerializeField] int timeBetweenPuffsMax = 9;

	private bool isAlive = true;

	private bool wallUp = false;
	private bool wallDown = false;
	private bool wallRight = false;
	private bool wallLeft = false;


	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
		myAnimator = GetComponent<Animator>();
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
		RaycastForWalls();

		var xVelocity = Random.Range(-5, 5);
		var yVelocity = Random.Range(-5, 5);
		
		if (wallUp)
		{
			yVelocity = Random.Range(-3, -5);
		}
		if (wallDown)
		{
			yVelocity = Random.Range(3, 5);
		}
		if (wallLeft)
		{
			xVelocity = Random.Range(3, 5);
		}
		if (wallRight)
		{
			xVelocity = Random.Range(-3, -5);
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

	void RaycastForWalls()
	{
		RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 2f, LayerMask.GetMask("Wall"));
		RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Wall"));
		RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 2f, LayerMask.GetMask("Wall"));
		RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 2f, LayerMask.GetMask("Wall"));

		if (hitUp) { wallUp = true;}
		else { wallUp = false;}

		if (hitDown) { wallDown = true;}
		else {wallDown = false;}

		if (hitRight) { wallRight = true;}
		else {wallRight = false;}

		if (hitLeft) { wallLeft = true;}
		else { wallLeft = false;}
	}
}
