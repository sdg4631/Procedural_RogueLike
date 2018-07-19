using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ATTACH TO PROJECTILE PREFAB
public class EnemyProjectileDestroyer : MonoBehaviour 
{
	

	void Start() 
	{
		
	}
	

	void Update() 
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
			print("hit!");
		}
	}
}
