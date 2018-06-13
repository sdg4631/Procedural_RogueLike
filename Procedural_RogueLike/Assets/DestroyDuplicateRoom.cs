using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicateRoom : MonoBehaviour 
{
	void Start()
	{
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == gameObject.tag)
		{
			Destroy(gameObject);
			print(gameObject + " destroyed");
		}
	}
}
