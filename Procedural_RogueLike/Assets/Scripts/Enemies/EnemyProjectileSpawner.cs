using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSpawner: MonoBehaviour 
{
	[SerializeField] public string projectileTag;

	[SerializeField] float projectileSpeed;

	[SerializeField] bool shootNorth = false;
	[SerializeField] bool shootNorthEast = false;
	[SerializeField] bool shootEast = false;
	[SerializeField] bool shootSouthEast = false;
	[SerializeField] bool shootSouth = false;
	[SerializeField] bool shootSouthWest = false;
	[SerializeField] bool shootWest = false;
	[SerializeField] bool shootNorthWest = false;

	[SerializeField] bool isRotating = false;

	void Fireprojectile()
	{
		if (shootNorth)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1 * projectileSpeed);	
		}
		if (shootNorthEast)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, 1 * projectileSpeed);

			if (isRotating)
			{
				
			}
		}
		if (shootEast)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, 0);
		}
		if (shootSouthEast)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(1 * projectileSpeed, -1 * projectileSpeed);
		}
		if (shootSouth)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1 * projectileSpeed);
		}
		if (shootSouthWest)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, -1 * projectileSpeed);
		}
		if (shootWest)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, 0);
		}
		if (shootNorthWest)
		{
			GameObject projectile = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
			projectile.transform.position = transform.position;
			projectile.transform.rotation = transform.rotation;
			projectile.SetActive(true);
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * projectileSpeed, 1 * projectileSpeed);
		}
	}
}
