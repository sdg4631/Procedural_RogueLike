using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCorrector : MonoBehaviour 
{
	private RoomSpawner roomSpawner;
	private SearchForNeighborRooms searchForNeighbors;
	private RoomTemplates templates;

	private float waitTime = 5f;

	void Start() 
	{
		searchForNeighbors = GetComponent<SearchForNeighborRooms>();
		templates = FindObjectOfType<RoomTemplates>();
		Invoke("ReplaceSpawnedRoom", 4f);
		Destroy(this, waitTime);
	}
	

	// Replaces spawned room with correct doors to adjacent rooms
	void ReplaceSpawnedRoom()
	{

		if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{	
			if (gameObject.tag != "TB")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TB, transform.position, Quaternion.identity);	
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}					
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{	
			if (gameObject.tag != "TL")	
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TL, transform.position, Quaternion.identity);	
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}				
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "TR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "TBL")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TBL, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "TBR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TBR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}		
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "TLR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TLR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "BL")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.BL, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "BR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.BR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "BLR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.BLR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "LR")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.LR, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == true && gameObject.tag != "Entry")
		{
			if (gameObject.tag != "TBRL")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.TBRL, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}	
		else if (searchForNeighbors.upNeighbor == true && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "T")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.T, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == true && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "B")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.B, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == true && searchForNeighbors.leftNeighbor == false)
		{
			if (gameObject.tag != "R")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.R, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else if (searchForNeighbors.upNeighbor == false && searchForNeighbors.downNeighbor == false && searchForNeighbors.rightNeighbor == false && searchForNeighbors.leftNeighbor == true)
		{
			if (gameObject.tag != "L")
			{
				templates.rooms.Remove(gameObject);
				Destroy(gameObject);
				var newRoom = Instantiate(templates.L, transform.position, Quaternion.identity);
				Destroy(newRoom.GetComponent<DoorCorrector>());	
				print("Replaced room at: " + transform.position + " with" + newRoom);
			}			
		}
		else
		{

		}
	}
}
