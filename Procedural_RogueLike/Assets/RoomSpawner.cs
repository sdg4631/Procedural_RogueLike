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
	private SearchForNeighborRooms searchForNeighbors;
	private int random;
	private bool spawned = false;

	BoxCollider2D myCollider;
	

	// // Used to determine the correct room while error correcting for Spawn()
	// private bool searchForNeighbors.upNeighbor = false;
	// private bool searchForNeighbors.downNeighbor = false;
	// private bool searchForNeighbors.rightNeighbor = false;
	// private bool searchForNeighbors.leftNeighbor = false;


	void Start() 
	{
		myCollider = GetComponent<BoxCollider2D>();
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		searchForNeighbors = GetComponentInParent<SearchForNeighborRooms>();
		
		Invoke("Spawn", 0.1f);

	}

	void Update()
	{
		
	}

	void Spawn() 
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Entry"))) { return; }
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("SpawnedRoom"))) { return; }

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
		// else if (spawned == false && templates.rooms.Count - 1 >= templates.approxNumOfRooms)
		// {
		// 	if (openingDirection == 1)
		// 	{
		// 		// need to spawn a room with ONLY A BOTTOM door
		// 		spawnedRoom = Instantiate(templates.closedBottomRoom, transform.position, Quaternion.identity);
		// 	}
		// 	else if (openingDirection == 2)
		// 	{
		// 		// need to spawn a room with ONLY A TOP door
		// 		spawnedRoom = Instantiate(templates.closedTopRoom, transform.position, Quaternion.identity);
		// 	}
		// 	else if (openingDirection == 3)
		// 	{
		// 		// need to spawn a room with ONLY A LEFT door
		// 		spawnedRoom = Instantiate(templates.closedLeftRoom, transform.position, Quaternion.identity);
		// 	}
		// 	else if (openingDirection == 4)
		// 	{
		// 		// need to spawn a room with ONLY A RIGHT door
		// 		spawnedRoom = Instantiate(templates.closedRightRoom, transform.position, Quaternion.identity);
		// 	}
		// }
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
										
			}
			else if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
			{
				Destroy(gameObject);
			}
			spawned = true;
		}
	}
}
