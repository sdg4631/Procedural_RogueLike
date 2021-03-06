﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour 
{
	[SerializeField] AudioClip chestOpenSFX;

	Animator myAnimator;
	AudioSource audioSource;
	bool isChestOpen = false;

	void Start() 
	{
		myAnimator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
	}
	

	void Update() 
	{
		
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player") // TODO add if statement to check if player has a key
		{
			if (isChestOpen == false)
			{
				audioSource.PlayOneShot(chestOpenSFX);
				myAnimator.SetBool("chestOpen", true);
				isChestOpen = true;
			}
		}
	}

	void SetChestOpen()
	{
		var closedChest = gameObject.transform.GetChild(0);
		var openChest = gameObject.transform.GetChild(1);
		var closedChestFX = gameObject.transform.GetChild(2);
		var openChestFX = gameObject.transform.GetChild(3);

		closedChest.gameObject.SetActive(false);
		openChest.gameObject.SetActive(true);
		openChestFX.gameObject.SetActive(true);
		closedChestFX.gameObject.SetActive(false);
	}
}
