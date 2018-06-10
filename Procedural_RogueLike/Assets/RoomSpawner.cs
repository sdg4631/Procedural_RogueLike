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

	private RoomTemplates templates;
	private int random;
	private bool spawned = false;


	void Start() 
	{
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke("Spawn", 0.05f);
	}
	

	void Spawn() 
	{
		if (spawned == false)
		{
			if (openingDirection == 1)
			{
				// need to spawn room with a BOTTOM door
				random = Random.Range(0, templates.bottomRooms.Length);
				Instantiate(templates.bottomRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 2)
			{
				// need to spawn room with a TOP door
				random = Random.Range(0, templates.topRooms.Length);
				Instantiate(templates.topRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 3)
			{
				// need to spawn room with a LEFT door
				random = Random.Range(0, templates.leftRooms.Length);
				Instantiate(templates.leftRooms[random], transform.position, Quaternion.identity);
			}
			else if (openingDirection == 4)
			{
				// need to spawn room with a RIGHT door
				random = Random.Range(0, templates.rightRooms.Length);
				Instantiate(templates.rightRooms[random], transform.position, Quaternion.identity);
			}
		}
		spawned = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("SpawnPoint"))
		{
			if (openingDirection == 0)
			{
				Destroy(other.gameObject);
			}
			else if (other.GetComponent<RoomSpawner>().openingDirection == 0)
			{
				Destroy(gameObject);
			}
			else if (other.GetComponent<RoomSpawner>().openingDirection !=0 && openingDirection != 0)
			{
				other.GetComponent<RoomSpawner>().spawned = true;
				spawned = true;
				
			}










		// 	if (other.GetComponent<RoomSpawner>().spawned == false && spawned == false)
		// 	{
		// 		// spawn walls blocking off any openings
		// 		Instantiate(templates.closedRoom, transform.position, Quaternion.identity);	
		// 		Destroy(gameObject);	
		// 	}
		// spawned = true;
		}
	}
}
