using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCorrector : MonoBehaviour 
{
	RoomSpawner roomSpawner;
	SearchForNeighborRooms searchForNeighbors;
	RoomTemplates templates;

	void Start() 
	{
		searchForNeighbors = GetComponent<SearchForNeighborRooms>();
		templates = FindObjectOfType<RoomTemplates>();
		Invoke("ReplaceSpawnedRoom", 7f);
	}
	

	void Update() 
	{
		
	}

// Replaces spawned room with correct doors to adjacent rooms
	void ReplaceSpawnedRoom()
	{
		if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{			
			var newRoom = Instantiate(templates.TB, transform.position, Quaternion.identity);			
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{			
			var newRoom = Instantiate(templates.TL, transform.position, Quaternion.identity);	
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.TR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.TBL, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.TBR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.TLR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.BL, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.BR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.BLR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.LR, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true && gameObject.tag != "Entry")
		{
			var newRoom = Instantiate(templates.TBRL, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}	
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.closedTopRoom, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.closedBottomRoom, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			var newRoom = Instantiate(templates.closedRightRoom, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			var newRoom = Instantiate(templates.closedLeftRoom, transform.position, Quaternion.identity);
			print("Replaced room at: " + transform.position + " with" + newRoom);
			templates.rooms.Remove(this.gameObject);
			Destroy(this.gameObject);
		}

	}
}
