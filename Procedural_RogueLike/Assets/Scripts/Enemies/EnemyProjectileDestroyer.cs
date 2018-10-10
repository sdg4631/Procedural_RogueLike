using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ATTACH TO PROJECTILE PREFAB
public class EnemyProjectileDestroyer : MonoBehaviour 
{
	// To be set in the inspector
	[SerializeField] public string splashProjectileTag; 
	private GameObject splash;

	PolygonCollider2D myCollider;

	void Start()
	{
		myCollider = GetComponent<PolygonCollider2D>();
	}

	void FixedUpdate()
	{
		WhenCollisionOccurs();
	}


	void WhenCollisionOccurs()
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Player", "ProjectileWall", "Chest", "StaticObject")))
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
