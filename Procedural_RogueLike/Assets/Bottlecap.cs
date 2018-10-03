using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottlecap : MonoBehaviour 
{
	[SerializeField] GameObject fizzFX;
	[SerializeField] AudioClip pickupSFX;

	AudioSource audioSource;
	BoxCollider2D mycollider;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		mycollider = GetComponent<BoxCollider2D>();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			audioSource.PlayOneShot(pickupSFX);
			var fizz = Instantiate(fizzFX, transform.position, fizzFX.transform.rotation);
			mycollider.enabled = false;
			gameObject.transform.GetChild(0).gameObject.SetActive(false);
			Destroy(fizz, 2f);
			Destroy(gameObject, 2f);
		}
	}
}
