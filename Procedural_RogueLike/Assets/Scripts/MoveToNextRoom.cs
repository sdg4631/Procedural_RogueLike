using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToNextRoom : MonoBehaviour 
{

	[SerializeField] bool topDoor = false;
	[SerializeField] bool botDoor = false;
	[SerializeField] bool rightDoor = false;
	[SerializeField] bool leftDoor = false;


	[SerializeField] float cameraSpeed = 1.5f;
	
	private Image blackScreen = null;

	CameraShake mainCamera;
	PlayerMovement player;

	float stallPitTimer = 0f;
	float stallDuration = .5f;

	void Awake()
	{
		GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
		blackScreen = canvas.GetComponentInChildren<Image>(includeInactive: true);

		blackScreen.canvasRenderer.SetAlpha(0f);
		blackScreen.gameObject.SetActive(true);

		player = FindObjectOfType<PlayerMovement>();
	}

	void Update()
	{
		StartCoroutine(StallPit());
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
        {
            TransferPlayer(other);
			StartCoroutine(MoveCamera());
			FadeIn();
			Invoke("FadeOut", .1f);
			player.changingRooms = true;
						
        }
    }

	IEnumerator StallPit()
	{
		if (player.changingRooms == true)
		{
			player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			player.pitRoot.GetComponent<Animator>().SetBool("isRunning", false);


			stallPitTimer += Time.deltaTime;
			if (stallPitTimer >= stallDuration)
			{
				player.changingRooms = false;
				
			}
		}
		else 
		{
			stallPitTimer = 0.0f;
		}
		yield return 0;
	}

    void TransferPlayer(Collider2D other)
    {		
		if (topDoor)
		{
			other.transform.position += new Vector3(0, 4);
		}
		else if (botDoor)
		{
			other.transform.position += new Vector3(0, -4);
		}
		else if (rightDoor)
		{
			other.transform.position += new Vector3(4, 0);
		}
		else if (leftDoor)
		{
			other.transform.position += new Vector3(-4, 0);
		}
    }

    IEnumerator MoveCamera()
	{
		mainCamera = FindObjectOfType<CameraShake>();

		if (topDoor)
		{
			var timeToStart = Time.time;
			var currentPos = mainCamera.transform.position;
			var targetPos = mainCamera.transform.position +=  new Vector3(0, 10.8f);

			float tParam = 0f;

			while (tParam < 1.0f)
			{
				tParam += Time.deltaTime * cameraSpeed;
				mainCamera.transform.position = Vector3.Lerp(currentPos, targetPos, tParam);

				yield return 0;
			}
		}
		else if (botDoor)
		{
			var timeToStart = Time.time;
			var currentPos = mainCamera.transform.position;
			var targetPos = mainCamera.transform.position +=  new Vector3(0, -10.8f);

			float tParam = 0f;

			while (tParam < 1.0f)
			{
				tParam += Time.deltaTime * cameraSpeed;
				mainCamera.transform.position = Vector3.Lerp(currentPos, targetPos, tParam);

				yield return 0;
			}
		}
		else if (rightDoor)
		{
			var timeToStart = Time.time;
			var currentPos = mainCamera.transform.position;
			var targetPos = mainCamera.transform.position +=  new Vector3(19.2f, 0);

			float tParam = 0f;

			while (tParam < 1.0f)
			{
				tParam += Time.deltaTime * cameraSpeed;
				mainCamera.transform.position = Vector3.Lerp(currentPos, targetPos, tParam);

				yield return 0;
			}
		}
		else if (leftDoor)
		{
			var timeToStart = Time.time;
			var currentPos = mainCamera.transform.position;
			var targetPos = mainCamera.transform.position +=  new Vector3(-19.2f, 0);

			float tParam = 0f;

			while (tParam < 1.0f)
			{
				tParam += Time.deltaTime * cameraSpeed;
				mainCamera.transform.position = Vector3.Lerp(currentPos, targetPos, tParam);

				yield return 0;
			}
		}			
	}

	void FadeIn()
	{
		blackScreen.CrossFadeAlpha(.4f, .1f, true);
	}

	void FadeOut()
	{
		blackScreen.CrossFadeAlpha(0f, .15f, true);
	}
}
