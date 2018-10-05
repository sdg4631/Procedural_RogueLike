using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAndCollisionManager : MonoBehaviour 
{
	[SerializeField] int maxHealth;
	[SerializeField] public int currentHealth;
	[SerializeField] GameObject deathFXPrefab;

	[SerializeField] GameObject tabsPrefab = null;

	SpawnEnemies enemySpawner;

	void Start()
	{
		currentHealth = maxHealth;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Dash")
		{	
			Destroy(gameObject);
		}
	}

	public void ApplyDamage(int projectileDamage)
	{
		currentHealth = currentHealth - projectileDamage;

		if (currentHealth <= 0)
		{
			float deathDelay = 0f;
			Invoke("Die", deathDelay);
		}
	}

	void Die()
	{
		var deathFX = Instantiate(deathFXPrefab, transform.position, Quaternion.identity);

		float amount = Random.Range(0f, 6f);
		for (int i = 0; i < amount; i++)
		{
			if (tabsPrefab != null)
			{
				var tabs = Instantiate(tabsPrefab, transform.position, Quaternion.identity);
			}
			
		}

		Destroy(deathFX, 2f);
		Destroy(gameObject, .05f);		 
	}


}
