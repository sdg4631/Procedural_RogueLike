using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForNeighborRooms : MonoBehaviour 
{
	public bool upNeighbor = false;
	public bool downNeighbor = false;
	public bool rightNeighbor = false;
	public bool leftNeighbor = false;

	private float waitTime = 5f;

	void Start() 
	{
		Destroy(this, waitTime);
	}
	
	void Update() 
	{
		RaycastForAdjacentRooms();
	}

	void RaycastForAdjacentRooms()
	{		
		RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 11f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 11f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 20f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 20f, LayerMask.GetMask("SpawnedRoom"));

		// Raycast separately for ENTRY room 
		RaycastHit2D hitUpEntry = Physics2D.Raycast(transform.position, Vector2.up, 11f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitDownEntry = Physics2D.Raycast(transform.position, Vector2.down, 11f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitRightEntry = Physics2D.Raycast(transform.position, Vector2.right, 20f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitLeftEntry = Physics2D.Raycast(transform.position, Vector2.left, 20f, LayerMask.GetMask("Entry"));

		// Look for UP neighbor
		if (hitUp.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an UPSIDE neighbor at " + hitUp.collider.transform.position);
			upNeighbor = true;
		}
		else if (hitUpEntry.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an UPSIDE neighbor at " + hitUpEntry.collider.transform.position);
			upNeighbor = true;
		}

		// Look for DOWN neighbor
		if (hitDown.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an DOWNSIDE neighbor at " + hitDown.collider.transform.position);
			downNeighbor = true;
		}
		else if (hitDownEntry.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an DOWNSIDE neighbor at " + hitDownEntry.collider.transform.position);
			downNeighbor = true;
		}

		// Look for RIGHT neighbor
		if (hitRight.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an RIGHTSIDE neighbor at " + hitRight.collider.transform.position);
			rightNeighbor = true;
		}
		else if (hitRightEntry.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an RIGHTSIDE neighbor at " + hitRightEntry.collider.transform.position);
			rightNeighbor = true;
		}

		// Look for LEFT neighbor
		if (hitLeft.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an LEFTSIDE neighbor at " + hitLeft.collider.transform.position);
			leftNeighbor = true;
		}			
		else if (hitLeftEntry.collider != null)
		{
			// print("SpawnedRoom at: " + transform.position + "  has an LEFTSIDE neighbor at " + hitLeftEntry.collider.transform.position);
			leftNeighbor = true;
		}
	}
}
