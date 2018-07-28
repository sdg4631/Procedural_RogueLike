using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour 
{
	public bool wallUp = false;
	public bool wallDown = false;
	public bool wallRight = false;
	public bool wallLeft = false;

	public bool raycastWalls = true;

	void Start() 
	{
		
	}
	

	void Update() 
	{
		RaycastForWalls();
	}

	public void RaycastForWalls()
	{
		if (raycastWalls != true) { return; }

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
