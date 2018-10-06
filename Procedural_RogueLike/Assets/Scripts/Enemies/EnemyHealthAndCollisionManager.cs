using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAndCollisionManager : MonoBehaviour 
{
	[SerializeField] int maxHealth;
	[SerializeField] public int currentHealth;
	[SerializeField] GameObject deathFXPrefab;
	[SerializeField] GameObject tabsPrefab = null;
	
	Rigidbody2D myRigidBody;
	PolygonCollider2D myCollider;
	SpriteRenderer[] sprites = null;
	SpawnEnemies enemySpawner;

	

	void Start()
	{
		currentHealth = maxHealth;
		myCollider = GetComponent<PolygonCollider2D>();
		myRigidBody = GetComponent<Rigidbody2D>();
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

		// float amount = Random.Range(0f, 6f);
		// for (int i = 0; i < amount; i++)
		// {
		// 	if (tabsPrefab != null)
		// 	{
		// 		var tabs = Instantiate(tabsPrefab, transform.position, Quaternion.identity);
		// 	}		
		// }

		Destroy(deathFX, 2f);
		Destroy(gameObject, .05f);		 
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("PlayerProjectile")))
		{      
			if (currentHealth > other.GetComponent<PlayerProjectileStats>().projectileDamage)
			{
				StartCoroutine(Blink(.03f, 1, Color.red, .5f, gameObject));
				StartCoroutine(Knockback(other.gameObject));
				ApplyDamage(other.GetComponent<PlayerProjectileStats>().projectileDamage);
			}
			else
			{
				StartCoroutine(Blink(.03f, 1, Color.red, .75f, gameObject));
				//StartCoroutine(DeathKnockback(other.gameObject));
				ApplyDamage(other.GetComponent<PlayerProjectileStats>().projectileDamage);
			}
        }	
	}


 	IEnumerator Knockback(GameObject other)
    {
		if (gameObject != null)
		{
			myRigidBody.AddForce(transform.forward * other.GetComponent<PlayerProjectileStats>().projectileForce, ForceMode2D.Impulse);
			// gameObject.GetComponent<Rigidbody2D>().drag = other.GetComponent<PlayerProjectileStats>().targetDrag;
			yield return new WaitForSeconds(.5f);

			// if (gameObject != null)
			// {
			// 	//other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			// 	gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
			// 	gameObject.GetComponent<Rigidbody2D>().drag = 0.1f;
			// }
		}
    }

	IEnumerator DeathKnockback(GameObject other)
    {
		if (gameObject != null)
		{
			gameObject.GetComponent<Rigidbody2D>().AddForce(transform.forward * other.GetComponent<PlayerProjectileStats>().killingBlowForce, ForceMode2D.Impulse);
			gameObject.GetComponent<Rigidbody2D>().drag = other.GetComponent<PlayerProjectileStats>().targetDrag;
			yield return new WaitForSeconds(.5f);
		}
    }

	IEnumerator Blink(float delayBetweenBlinks, int numberOfBlinks, Color blinkColor, float alpha, GameObject obj)
	{
		var sprites = obj.GetComponentsInChildren<SpriteRenderer>();
		var counter = 0;

		while (counter <= numberOfBlinks)
		{
			foreach(var sprite in sprites)
			{
				if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
				{
					blinkColor.a =  alpha;
					sprite.material.SetColor("_BlinkColor", blinkColor);	
				}
			}
			counter++;
			yield return new WaitForSeconds(delayBetweenBlinks);
		}

		// revert to our standard sprite color
		foreach (var sprite in sprites)
		{
			if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
			{
				blinkColor.a = 0f;
				sprite.material.SetColor("_BlinkColor", blinkColor);
			}
		}		
	}

	IEnumerator BlinkSmooth( float timeScale, float duration, Color blinkColor, GameObject obj )
	{
		var sprites = obj.GetComponentsInChildren<SpriteRenderer>();
		var elapsedTime = 0f;

		while( elapsedTime <= duration )
		{
			foreach(var sprite in sprites)
			{
				if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
				{
					blinkColor.a = Mathf.PingPong( elapsedTime * timeScale, 1f );
					sprite.material.SetColor( "_BlinkColor", blinkColor );
				}	
				elapsedTime += Time.deltaTime;	
			}
			
			yield return null;
		}

		// revert to our standard sprite color
		foreach(var sprite in sprites)
		{
			if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
			{
				blinkColor.a = 0f;
				sprite.material.SetColor( "_BlinkColor", blinkColor );
			}
			
		}
		
	}

}
