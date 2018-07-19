using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawner : MonoBehaviour 
{
	[SerializeField] GameObject projectilePrefab = null;
	[SerializeField] float projectileSpeed;

	[SerializeField] bool shootNorth = false;
	[SerializeField] bool shootNorthEast = false;
	[SerializeField] bool shootEast = false;
	[SerializeField] bool shootSouthEast = false;
	[SerializeField] bool shootSouth = false;
	[SerializeField] bool shootSouthWest = false;
	[SerializeField] bool shootWest = false;
	[SerializeField] bool shootNorthWest = false;

	float destroyDelay = 10f;

	void Start() 
	{
		
	}
	

	void Update() 
	{
		
	}

	void FireProjectile()
	{
		if (shootNorth)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 * projectileSpeed);	
			Destroy(proj, destroyDelay);
		}
		if (shootNorthEast)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, 1 * projectileSpeed);
			Destroy(proj, destroyDelay);
		}
		if (shootEast)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, 0);
			Destroy(proj, destroyDelay);
		}
		if (shootSouthEast)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, -1 * projectileSpeed);
			Destroy(proj, destroyDelay);
		}
		if (shootSouth)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed);
			Destroy(proj, destroyDelay);
		}
		if (shootSouthWest)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, -1 * projectileSpeed);
			Destroy(proj, destroyDelay);
		}
		if (shootWest)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, 0);
			Destroy(proj, destroyDelay);
		}
		if (shootNorthWest)
		{
			var proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
			proj.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, 1 * projectileSpeed);
			Destroy(proj, destroyDelay);
		}
	}

}
