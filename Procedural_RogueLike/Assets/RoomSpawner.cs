using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour 
{
	public int openingDirection;
	// 1 --> need bottom door
	// 2 --> need top door
	// 3 --> need left door
	// 4 --> need right door

	
	public GameObject spawnedRoom = null;
	public float replaceSpawnedRoomDelay = 7f; 

	private RoomTemplates templates;
	private int random;
	private bool spawned = false;
	private bool replacedRoom = false;
	

	// Used to determine the correct room while error correcting for Spawn()
	private bool upNeighbor = false;
	private bool downNeighbor = false;
	private bool rightNeighbor = false;
	private bool leftNeighbor = false;


	void Start() 
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke("Spawn", 0.1f);
		Invoke("ReplaceSpawnedRoom", 5f);
	}

	void Update()
	{
		RaycastForAdjacentRooms();
	}

	void Spawn() 
	{
		if (spawned == false && templates.rooms.Count - 1 < templates.approxNumOfRooms)
		{
			if (openingDirection == 1)
			{
				// need to spawn room with a BOTTOM door
				random = Random.Range(0, templates.bottomRooms.Length);
				spawnedRoom = Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 2)
			{
				// need to spawn room with a TOP door
				random = Random.Range(0, templates.topRooms.Length);
				spawnedRoom = Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 3)
			{
				// need to spawn room with a LEFT door
				random = Random.Range(0, templates.leftRooms.Length);
				spawnedRoom = Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 4)
			{
				// need to spawn room with a RIGHT door
				random = Random.Range(0, templates.rightRooms.Length);
				spawnedRoom = Instantiate(templates.rightRooms[random], transform.position, Quaternion.identity);
			}
		}
		else if (spawned == false && templates.rooms.Count - 1 >= templates.approxNumOfRooms)
		{
			if (openingDirection == 1)
			{
				// need to spawn a room with ONLY A BOTTOM door
				spawnedRoom = Instantiate(templates.closedBottomRoom, transform.position, Quaternion.identity);
			}
			else if (openingDirection == 2)
			{
				// need to spawn a room with ONLY A TOP door
				spawnedRoom = Instantiate(templates.closedTopRoom, transform.position, Quaternion.identity);
			}
			else if (openingDirection == 3)
			{
				// need to spawn a room with ONLY A LEFT door
				spawnedRoom = Instantiate(templates.closedLeftRoom, transform.position, Quaternion.identity);
			}
			else if (openingDirection == 4)
			{
				// need to spawn a room with ONLY A RIGHT door
				spawnedRoom = Instantiate(templates.closedRightRoom, transform.position, Quaternion.identity);
			}
		}
		spawned = true;
	}

	// If two spawn points collide 
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("SpawnPoint"))
		{
			if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
			{
				// spawn walls blocking off any openings
				ReplaceSpawnedRoom(); // TODO workout how to properly spawn closed outer rooms						
			}
			else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
			{
				Destroy(gameObject);
			}
			spawned = true;
		}
	}

	void RaycastForAdjacentRooms()
	{
		if (spawnedRoom != null)
		{
			RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, LayerMask.GetMask("SpawnedRoom"));
			RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("SpawnedRoom"));
			RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("SpawnedRoom"));
			RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("SpawnedRoom"));
			if (hitUp.collider != null)
			{
				print("SpawnedRoom at: " + transform.position + "  has an UPSIDE neighbor at " + hitUp.collider.transform.position);
				upNeighbor = true;
			}

			if (hitDown.collider != null)
			{
				print("SpawnedRoom at: " + transform.position + "  has an DOWNSIDE neighbor at " + hitDown.collider.transform.position);
				downNeighbor = true;
			}

			if (hitRight.collider != null)
			{
				print("SpawnedRoom at: " + transform.position + "  has an RIGHTSIDE neighbor at " + hitRight.collider.transform.position);
				rightNeighbor = true;
			}

			if (hitLeft.collider != null)
			{
				print("SpawnedRoom at: " + transform.position + "  has an LEFTSIDE neighbor at " + hitLeft.collider.transform.position);
				leftNeighbor = true;
			}
		}	
	}

	// Replaces spawned room with correct doors to adjacent rooms
	void ReplaceSpawnedRoom()
	{
		if (upNeighbor == true && downNeighbor == true && rightNeighbor == false && leftNeighbor == false)
		{
			spawnedRoom = Instantiate(templates.TB, transform.position, Quaternion.identity);			
			print("Replaced room at: " + transform.position + " with" + spawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == false && rightNeighbor == false && leftNeighbor == true)
		{
			spawnedRoom = Instantiate(templates.TL, transform.position, Quaternion.identity);	
			print("Replaced room at: " + transform.position + " with" + spawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == false && rightNeighbor == true && leftNeighbor == false)
		{
			spawnedRoom = Instantiate(templates.TR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + spawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == true && rightNeighbor == false && leftNeighbor == true)
		{
			spawnedRoom = Instantiate(templates.TBL, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + spawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == true && rightNeighbor == true && leftNeighbor == false)
		{
			var newSpawnedRoom = Instantiate(templates.TBR, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == false && rightNeighbor == true && leftNeighbor == true)
		{
			var newSpawnedRoom = Instantiate(templates.TLR, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == false && downNeighbor == true && rightNeighbor == false && leftNeighbor == true)
		{
			var newSpawnedRoom = Instantiate(templates.BL, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == false && downNeighbor == true && rightNeighbor == true && leftNeighbor == false)
		{
			var newSpawnedRoom = Instantiate(templates.BR, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == false && downNeighbor == true && rightNeighbor == true && leftNeighbor == true)
		{
			var newSpawnedRoom = Instantiate(templates.BLR, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == false && downNeighbor == false && rightNeighbor == true && leftNeighbor == true)
		{
			var newSpawnedRoom = Instantiate(templates.LR, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		else if (upNeighbor == true && downNeighbor == true && rightNeighbor == true && leftNeighbor == true)
		{
			var newSpawnedRoom = Instantiate(templates.TBRL, transform.position, Quaternion.identity);
			Destroy(spawnedRoom);
			print("Replaced room at: " + transform.position + " with" + newSpawnedRoom);
		}
		spawned = true;			
	}
}
