using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ATTACH TO PROJECTILE PREFAB
public class EnemyProjectileDestroyer : MonoBehaviour 
{
	// To be set in the inspector
	[SerializeField] public string splashProjectileTag; 
	private GameObject splash;


	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
		{	
			// Spawn Splash
			splash = ObjectPooler.SharedInstance.GetPooledObject(splashProjectileTag);
			splash.transform.position = transform.position;
			splash.transform.rotation = transform.rotation;
			splash.SetActive(true);	
	
			this.gameObject.SetActive(false);	
		}
	}
}
