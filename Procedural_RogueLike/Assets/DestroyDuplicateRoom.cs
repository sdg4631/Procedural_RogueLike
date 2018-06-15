using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDuplicateRoom : MonoBehaviour 
{
	BoxCollider2D myCollider;
	RoomTemplates templates;

	void Start()
	{
		myCollider = GetComponent<BoxCollider2D>();
		templates = FindObjectOfType<RoomTemplates>();
		Invoke("DestroyRoom", 5f);
	}

	void DestroyRoom()
	{
		var overlappingRooms = myCollider.IsTouchingLayers(LayerMask.GetMask("SpawnedRoom"));
		if (overlappingRooms)
		{
			print(gameObject + " destroyed");
			templates.rooms.Remove(gameObject);
			Destroy(gameObject);			
		}
	}
}
