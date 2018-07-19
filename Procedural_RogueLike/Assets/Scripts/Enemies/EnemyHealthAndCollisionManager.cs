using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAndCollisionManager : MonoBehaviour 
{
	[SerializeField] int maxHealth;
	[SerializeField] public int currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		Die();
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
	}

	void Die()
	{
		if (currentHealth <= 0)
		{
			Destroy(gameObject);
		} 
	}
}
