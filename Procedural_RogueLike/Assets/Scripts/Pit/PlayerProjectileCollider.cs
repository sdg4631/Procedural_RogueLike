using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileCollider : MonoBehaviour 
{
	SpriteRenderer[] sprites = null;
	[SerializeField] public int projectileDamage;
	[SerializeField] float projectileForce;
	[SerializeField] float killingBlowForce;
	[SerializeField] float targetDrag;

	EnemyHealthAndCollisionManager enemyHealth;

	void OnParticleCollision(GameObject other)
	{

		if (other.gameObject.tag == "Enemy")
		{      
			if (other.GetComponent<EnemyHealthAndCollisionManager>().currentHealth > projectileDamage)
			{
				StartCoroutine(Blink(.03f, 1, Color.red, .5f, other));
				//StartCoroutine(BlinkSmooth(1f, .5f, Color.red, other));
				StartCoroutine(Knockback(other));
				other.GetComponent<EnemyHealthAndCollisionManager>().ApplyDamage(projectileDamage);
			}
			else
			{
				StartCoroutine(Blink(.03f, 1, Color.red, .75f, other));
				//StartCoroutine(DeathKnockback(other));
				other.GetComponent<EnemyHealthAndCollisionManager>().ApplyDamage(projectileDamage);
			}
        }	
	}

    IEnumerator Knockback(GameObject other)
    {
		if (other != null)
		{
			other.GetComponent<Rigidbody2D>().AddForce(transform.forward * projectileForce, ForceMode2D.Impulse);
			other.GetComponent<Rigidbody2D>().drag = targetDrag;
			yield return new WaitForSeconds(.5f);

			if (other != null)
			{
				//other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				other.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				other.GetComponent<Rigidbody2D>().drag = 0.1f;
			}
		}
    }

	IEnumerator DeathKnockback(GameObject other)
    {
		if (other != null)
		{
			other.GetComponent<Rigidbody2D>().AddForce(transform.forward * killingBlowForce, ForceMode2D.Impulse);
			other.GetComponent<Rigidbody2D>().drag = targetDrag;
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
