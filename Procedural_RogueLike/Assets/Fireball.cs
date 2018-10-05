using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour 
{
	BoxCollider2D myCollider;

	void Start() 
	{
		myCollider = GetComponent<BoxCollider2D>();
	}
	

	void Update() 
	{
		SetInactiveOnHit();
	}

	void SetInactiveOnHit()
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Chest", "ProjectileWall")))
		{
			StartCoroutine(SetProjectileInactive(gameObject, .1f));	
		}
	}

	IEnumerator SetProjectileInactive(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
    }
}
