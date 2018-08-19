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
				StartCoroutine(BlinkRed(other));
				StartCoroutine(Knockback(other));
				other.GetComponent<EnemyHealthAndCollisionManager>().ApplyDamage(projectileDamage);
			}
			else
			{
				StartCoroutine(StayRed(other));
				StartCoroutine(DeathKnockback(other));
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
				other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
				other.GetComponent<Rigidbody2D>().angularVelocity = 0f;
				other.GetComponent<Rigidbody2D>().drag = 0.1f;
			}
		}
    }

    IEnumerator BlinkRed(GameObject gameObject)
	{
		sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
		
		foreach (var sprite in sprites)
		{
			if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
			{
				sprite.color = Color.red;
				yield return null;
			}				
		}
		foreach (var sprite in sprites)
		{
			yield return new WaitForSeconds(.01f);
			if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
			{
				sprite.color = Color.white;
			}		
		}
	}

	IEnumerator StayRed(GameObject gameObject)
	{
		sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
		
		foreach (var sprite in sprites)
		{
			if (sprite != null && sprite.tag != "EnemyEye" && sprite.tag != "EnemyLight")
			{
				sprite.color = Color.red;
				yield return null;
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
}
