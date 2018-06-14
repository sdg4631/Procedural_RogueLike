using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour 
{
	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;
	

	public GameObject closedBottomRoom;
	public GameObject closedTopRoom;
	public GameObject closedRightRoom;
	public GameObject closedLeftRoom;

	public GameObject TB;
	public GameObject TL;
	public GameObject TR;
	public GameObject TBL;
	public GameObject TBR;
	public GameObject TLR;
	public GameObject BL;
	public GameObject BR;
	public GameObject BLR;
	public GameObject LR;
	public GameObject TBRL;

	public List<GameObject> rooms;

	public int approxNumOfRooms = 8;
	public float waitTime;
	public bool spawnedBoss;
	public GameObject boss;

	void Update() 
	{
		if (waitTime <= 0 && spawnedBoss == false)
		{
			for (int i = 0; i < rooms.Count; i++)
			{
				if (i == rooms.Count - 1)
				{
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
			}
		}
		else
		{
			waitTime -= Time.deltaTime;
		}
	}
}
