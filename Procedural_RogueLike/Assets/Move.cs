using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour 
{
	Rigidbody2D rb;

	void Start() 
	{
		rb = GetComponent<Rigidbody2D>();
	}
	

	void Update() 
	{
		rb.velocity = new Vector2(5,0); 
	}
}
