using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchForNeighborRooms : MonoBehaviour 
{

	RoomSpawner roomSpawner;
	RoomTemplates templates;

	// Used to determine the correct room while error correcting for Spawn()
	private bool upNeighbor = false;
	private bool downNeighbor = false;
	private bool rightNeighbor = false;
	private bool leftNeighbor = false;

	void Start() 
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
	}
	

	void Update() 
	{
		RaycastForAdjacentRooms();
	}

	void RaycastForAdjacentRooms()
	{		
		RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("SpawnedRoom"));
		RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("SpawnedRoom"));

		// Raycast separately for ENTRY room 
		RaycastHit2D hitUpEntry = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitDownEntry = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitRightEntry = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("Entry"));
		RaycastHit2D hitLeftEntry = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Entry"));

		// Look for UP neighbor
		if (hitUp.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an UPSIDE neighbor at " + hitUp.collider.transform.position);
			upNeighbor = true;
		}
		else if (hitUpEntry.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an UPSIDE neighbor at " + hitUpEntry.collider.transform.position);
			upNeighbor = true;
		}

		// Look for DOWN neighbor
		if (hitDown.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an DOWNSIDE neighbor at " + hitDown.collider.transform.position);
			downNeighbor = true;
		}
		else if (hitDownEntry.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an DOWNSIDE neighbor at " + hitDownEntry.collider.transform.position);
			downNeighbor = true;
		}

		// Look for RIGHT neighbor
		if (hitRight.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an RIGHTSIDE neighbor at " + hitRight.collider.transform.position);
			rightNeighbor = true;
		}
		else if (hitRightEntry.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an RIGHTSIDE neighbor at " + hitRightEntry.collider.transform.position);
			rightNeighbor = true;
		}

		// Look for LEFT neighbor
		if (hitLeft.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an LEFTSIDE neighbor at " + hitLeft.collider.transform.position);
			leftNeighbor = true;
		}			
		else if (hitLeftEntry.collider != null)
		{
			print("SpawnedRoom at: " + transform.position + "  has an LEFTSIDE neighbor at " + hitLeftEntry.collider.transform.position);
			leftNeighbor = true;
		}
	}

	// 	// Replaces spawned room with correct doors to adjacent rooms
	// 	void ReplaceSpawnedRoom()
	// {
	// 	if (upNeighbor == true && downNeighbor == true && rightNeighbor == false && leftNeighbor == false)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TB, transform.position, Quaternion.identity);			
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == false && rightNeighbor == false && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TL, transform.position, Quaternion.identity);	
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == false && rightNeighbor == true && leftNeighbor == false)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == true && rightNeighbor == false && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TBL, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == true && rightNeighbor == true && leftNeighbor == false)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TBR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == false && rightNeighbor == true && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TLR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == false && downNeighbor == true && rightNeighbor == false && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.BL, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == false && downNeighbor == true && rightNeighbor == true && leftNeighbor == false)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.BR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == false && downNeighbor == true && rightNeighbor == true && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.BLR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == false && downNeighbor == false && rightNeighbor == true && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.LR, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	else if (upNeighbor == true && downNeighbor == true && rightNeighbor == true && leftNeighbor == true)
	// 	{
	// 		Destroy(spawnedRoom);
	// 		templates.rooms.Remove(spawnedRoom);
	// 		spawnedRoom = Instantiate(templates.TBRL, transform.position, Quaternion.identity);
	// 		print("Replaced room at: " + transform.position + " with" + spawnedRoom);
	// 	}
	// 	spawned = true;			
	// }
}
