using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour 
{
	[SerializeField] GameObject leafDissolvePrefab;

	Animator anim;
	PlayerHealthAndDamage playerHealth;
	int currentPlayerHealth;

	bool healthIncrease= false;
	bool healthDecrease= false;

	void Start() 
	{
		anim = GetComponent<Animator>();
		playerHealth = FindObjectOfType<PlayerHealthAndDamage>();
		currentPlayerHealth = playerHealth.health.CurrentVal;
	}
	
	void Update()
	{
		StartCoroutine(TrackCurrentPlayerHealth());

		CheckForHealthChanges();

		SetHealthAnimation();
	}
	
	IEnumerator TrackCurrentPlayerHealth()
	{
		yield return new WaitForSeconds(.01f);
		currentPlayerHealth = playerHealth.health.CurrentVal;
		Debug.Log(currentPlayerHealth);
		
	}

	void CheckForHealthChanges()
	{
		if (currentPlayerHealth > playerHealth.health.MaxVal) { return; }

		if (currentPlayerHealth < playerHealth.health.CurrentVal)
		{
			healthIncrease = true;
			Debug.Log("Health Increased");
		}
		else
		{
			healthIncrease = false;
		}
		
		if (currentPlayerHealth > playerHealth.health.CurrentVal)
		{
			healthDecrease = true;
			Debug.Log("Health Decreased");
		}
		else
		{
			healthDecrease = false;
		}
	}

	void SetLeafInactive(int childIndex)
	{
		transform.GetChild(childIndex).gameObject.SetActive(false);
	}

	void InstantiateLeafParticles(int childIndex)
	{
		// instantiate particles at leaf's transform position
		var leafDissolve = Instantiate(leafDissolvePrefab, transform.GetChild(childIndex).position, Quaternion.identity);
		
	}
	
	void SetHealthAnimation()
	{
		if(healthDecrease)
		{
			switch(playerHealth.health.CurrentVal)
			{
				case 6: 
					
					break;
				case 5:
					
					break;
				case 4:
					
					break;
				case 3:
					
					break;
				case 2:
					anim.Play("Shrivel Leaf 3");
					break;
				case 1:
					anim.Play("Shrivel Leaf 2");
					break;
				case 0:
					anim.Play("Shrivel Leaf 1");
					break;
				default:
					Debug.Log("Player health error");
					break;
			}	
		}
		else if (healthIncrease)
		{
			switch(playerHealth.health.CurrentVal)
			{
				case 6: 
					
					break;
				case 5:
					
					break;
				case 4:
					
					break;
				case 3:
					transform.GetChild(2).gameObject.SetActive(true);
					transform.GetChild(5).gameObject.SetActive(true);
					break;
				case 2:
					transform.GetChild(1).gameObject.SetActive(true);
					transform.GetChild(4).gameObject.SetActive(true);
					break;
				case 1:
					transform.GetChild(0).gameObject.SetActive(true);
					transform.GetChild(3).gameObject.SetActive(true);
					break;
				case 0:
					
					break;
				default:
					Debug.Log("Player health error");
					break;
			}	
		}
	}
}
