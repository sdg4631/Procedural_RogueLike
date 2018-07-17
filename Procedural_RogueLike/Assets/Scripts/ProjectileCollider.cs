using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour 
{
	SpriteRenderer[] sprites = null;


	void OnParticleCollision(GameObject other)
	{
		Rigidbody2D body = other.GetComponent<Rigidbody2D>();

		if (other.gameObject.tag == "Enemy")
		{
			if(body)
			{				
				StartCoroutine(BlinkRed(other));
			}
		}	
	}

	IEnumerator BlinkRed(GameObject gameObject)
	{
		sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
		
		foreach (var sprite in sprites)
		{
			if (sprite != null)
			{
				sprite.color = Color.red;
				yield return null;
			}
				
		}
		foreach (var sprite in sprites)
		{
			yield return new WaitForSeconds(.01f);
			if (sprite != null)
			{
				sprite.color = Color.white;
			}
			
		}

	}
}
