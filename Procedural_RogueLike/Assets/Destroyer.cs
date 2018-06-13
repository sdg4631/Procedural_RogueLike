using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour 
{
	private RoomTemplates templates;
	BoxCollider2D myCollider;
	
	void Start()
	{
		myCollider = GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "SpawnedRoom") // TODO figure out why room correction is causing bugs with the entry room
		{
			templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
			templates.rooms.Remove(other.gameObject);
			Destroy(other.gameObject);		
		}
		
	}
}
