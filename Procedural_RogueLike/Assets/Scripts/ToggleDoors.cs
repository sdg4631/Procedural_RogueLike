﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoors : MonoBehaviour 
{
	[SerializeField] Animator doors;
	SpawnEnemies enemySpawner;

	[SerializeField] bool isTopDoor = false;
	[SerializeField] bool isBotDoor = false;
	[SerializeField] bool isRightDoor = false;
	[SerializeField] bool isLeftDoor = false;

	[SerializeField] bool isEntryRoom = false;

	private float doorCloseDelay = .3f;

	private GameObject topDoor;
	private GameObject botDoor;
	private GameObject rightDoor;
	private GameObject leftDoor;

	
	void Start() 
	{
		topDoor = this.gameObject.transform.GetChild(0).gameObject;
		botDoor = this.gameObject.transform.GetChild(1).gameObject;
		rightDoor = this.gameObject.transform.GetChild(2).gameObject;
		leftDoor = this.gameObject.transform.GetChild(3).gameObject;

		enemySpawner = GetComponentInChildren<SpawnEnemies>();
	}
	

	void Update()
    {
        OpenDoors();
    }

    private void OpenDoors()
    {
        if (enemySpawner.enemiesCleared == true)
        {
            if (isTopDoor)
            {
                topDoor.SetActive(false);
            }
            if (isBotDoor)
            {
                botDoor.SetActive(false);
            }
            if (isRightDoor)
            {
                rightDoor.SetActive(false);
            }
            if (isLeftDoor)
            {
                leftDoor.SetActive(false);
            }

			doors.SetBool("Close", false);
            doors.SetBool("Open", true);
        }
    }

	private void CloseDoors()
	{
		if (enemySpawner.enemiesCleared == false && isEntryRoom == false)
		{
			if (isTopDoor)
            {
                topDoor.SetActive(true);
            }
            if (isBotDoor)
            {
                botDoor.SetActive(true);
            }
            if (isRightDoor)
            {
                rightDoor.SetActive(true);
            }
            if (isLeftDoor)
            {
                leftDoor.SetActive(true);
            }

			doors.SetBool("Open", false);
			doors.SetBool("Close", true);	
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Invoke("CloseDoors", doorCloseDelay);
		}
	}
}
