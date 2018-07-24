using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sap : MonoBehaviour 
{
	Animator myAnimator;
	Rigidbody2D myRigidBody;
	PlayerMovement player;

	bool isAlive = true;

	[SerializeField] int timeBetweenBounceMin = 1;
	[SerializeField] int timeBetweenBounceMax = 3;
	[SerializeField] float moveSpeed = 1f; 


	void Start() 
	{
		myAnimator = GetComponent<Animator>();
		myRigidBody = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<PlayerMovement>();
		StartCoroutine(StartBounceAnimation());
	}
	

	void Update() 
	{
		Move();
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

	void SetMoveSpeed(float newMoveSpeed)
	{
		moveSpeed = newMoveSpeed;
	}

	void Move()
	{
		if (Vector3.Distance(player.transform.position, transform.position) >= .6)
		{
			transform.position += (player.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
		}
		
	}

	void Stop()
	{
		myRigidBody.velocity = new Vector2(0,0);
		myAnimator.ResetTrigger("Bounce");
	}
}
