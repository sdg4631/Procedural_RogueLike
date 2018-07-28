using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileCollider : MonoBehaviour 
{
	SpriteRenderer[] sprites = null;
	[SerializeField] public int projectileDamage;
	[SerializeField] float projectileForce;
	[SerializeField] float targetDrag;

	EnemyHealthAndCollisionManager enemyHealth;

	void OnParticleCollision(GameObject other)
	{
		Rigidbody2D body = other.GetComponent<Rigidbody2D>();

		if (other.gameObject.tag == "Enemy")
		{
			if(body)
            {
                StartCoroutine(BlinkRed(other));
                StartCoroutine(Knockback(other));
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
			if (sprite != null && sprite.tag != "EnemyEye")
			{
				sprite.color = Color.red;
				yield return null;
			}
				
		}
		foreach (var sprite in sprites)
		{
			yield return new WaitForSeconds(.01f);
			if (sprite != null && sprite.tag != "EnemyEye")
			{
				sprite.color = Color.white;
			}
			
		}

	}
}
