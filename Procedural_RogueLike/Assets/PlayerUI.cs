using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour 
{
	Animator anim;
	PlayerHealthAndDamage playerHealth;

	void Start() 
	{
		anim = GetComponent<Animator>();
		playerHealth = FindObjectOfType<PlayerHealthAndDamage>();
	}
	
	void Update()
	{
		SetHealthAnimation();
	}

	void SetHealthAnimation()
	{
		switch(playerHealth.health.CurrentVal)
		{
			case 6: 

				break;
			case 5:
				anim.SetTrigger("5Health");
				transform.GetChild(2).gameObject.SetActive(true);
				break;
			case 4:
				transform.GetChild(2).gameObject.SetActive(false);
				transform.GetChild(5).gameObject.SetActive(false);
				break;
			case 3:
				anim.SetTrigger("3Health");
				transform.GetChild(1).gameObject.SetActive(true);
				break;
			case 2:
				transform.GetChild(1).gameObject.SetActive(false);
				transform.GetChild(4).gameObject.SetActive(false);
				break;
			case 1:
				anim.SetTrigger("1Health");
				transform.GetChild(0).gameObject.SetActive(true);
				break;
			case 0:
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetChild(3).gameObject.SetActive(false);
				break;
			default:
				Debug.Log("Player health error");
				break;
		}	
	}
}
