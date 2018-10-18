using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottlecap : MonoBehaviour 
{
	[SerializeField] GameObject fizzFX;
	[SerializeField] AudioClip pickupSFX;
	[SerializeField] int value;
	
	PlayerInventory playerInventory;
	

	AudioSource audioSource;
	BoxCollider2D mycollider;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		mycollider = GetComponent<BoxCollider2D>();
		playerInventory = FindObjectOfType<PlayerInventory>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerInventory.numOfBottlecaps += value;
			mycollider.enabled = false;
			audioSource.PlayOneShot(pickupSFX);
			var fizz = Instantiate(fizzFX, transform.position, fizzFX.transform.rotation);
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			Destroy(fizz, 2f);
			Destroy(gameObject, 2f);
		}
	}
}
