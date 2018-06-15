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

	private RoomTemplates templates;
	private BoxCollider2D myCollider;

	private int random;
	private bool spawned = false;
	private float waitTime = 6f;

	
	
	void Start() 
	{
		myCollider = GetComponent<BoxCollider2D>();
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		
		Invoke("Spawn", 0.3f);
		Destroy(gameObject, waitTime);
	}

	void Spawn() 
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Entry"))) { spawned = true; }
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("SpawnedRoom"))) { spawned = true; }

		if (spawned == false && templates.rooms.Count < templates.approxNumOfRooms)
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
		spawned = true;
	}

	// // If two spawn points collide 
	// void OnTriggerEnter2D(Collider2D other)
	// {
	// 	if (other.CompareTag("SpawnPoint"))
	// 	{			
	// 		if (other.GetComponent<RoomSpawner>().spawned == true && spawned == false)
	// 		{
	// 			Destroy(gameObject);
	// 		}
	// 		spawned = true;
	// 	}
	// }
}
