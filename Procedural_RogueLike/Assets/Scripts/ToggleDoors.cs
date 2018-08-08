using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDoors : MonoBehaviour 
{
	[SerializeField] Animator doors;

	[SerializeField] bool isTopDoor = false;
	[SerializeField] bool isBotDoor = false;
	[SerializeField] bool isRightDoor = false;
	[SerializeField] bool isLeftDoor = false;

	[SerializeField] bool isEntryRoom = false;

	[SerializeField] float doorCloseDelay = .5f;

	private GameObject topDoor;
	private GameObject botDoor;
	private GameObject rightDoor;
	private GameObject leftDoor;

	public bool enemiesCleared = false; //TODO Move this bool to a class that keeps track of enemies in respective room

	// TODO Remove later
	void DebugClearRoom()
	{
		if (Input.GetKeyDown("c"))
		{
			enemiesCleared = !enemiesCleared;
		}
	}


	void Start() 
	{
		topDoor = this.gameObject.transform.GetChild(0).gameObject;
		botDoor = this.gameObject.transform.GetChild(1).gameObject;
		rightDoor = this.gameObject.transform.GetChild(2).gameObject;
		leftDoor = this.gameObject.transform.GetChild(3).gameObject;
	}
	

	void Update()
    {
        OpenDoors();
		DebugClearRoom(); // TODO Remove later
    }

    private void OpenDoors()
    {
        if (enemiesCleared == true)
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
		if (enemiesCleared == false && isEntryRoom == false)
		{
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
