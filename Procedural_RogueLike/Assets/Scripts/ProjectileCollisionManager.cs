using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour 
{
	BoxCollider2D myCollider;
	Rigidbody2D myRigidBody;
	
	void Start() 
	{
		myCollider = GetComponent<BoxCollider2D>();
		myRigidBody = GetComponent<Rigidbody2D>();
		
	}
	
	void Update() 
	{		
		SetInactiveOnHit();
	}


	void SetInactiveOnHit()
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Chest", "ProjectileWall", "StaticObject")))
		{
			StartCoroutine(SetProjectileInactive(gameObject, 0));	
		}
	}

	IEnumerator SetProjectileInactive(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
    }


}
