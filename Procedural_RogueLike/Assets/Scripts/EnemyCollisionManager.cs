using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour 
{
	

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Dash")
		{	
			Destroy(gameObject);
		}
		else if (other.gameObject.tag == "Projectile")
		{
			
		}
	}

	
}
