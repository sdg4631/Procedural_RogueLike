using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionManager : MonoBehaviour 
{
	BoxCollider2D myCollider;
	Rigidbody2D myRigidBody;
	GameObject fxParent;
	
	[SerializeField] string onHitFXTag;
	
	void Start() 
	{
		myCollider = GetComponent<BoxCollider2D>();
		myRigidBody = GetComponent<Rigidbody2D>();

		fxParent = GameObject.FindGameObjectWithTag("FXParent");
		
	}
	
	void Update() 
	{		
		SetInactiveOnHit();
	}


	void SetInactiveOnHit()
	{
		if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Chest", "ProjectileWall", "StaticObject")))
		{
			// Activate On Hit FX
			GameObject onHitFX = ObjectPooler.SharedInstance.GetPooledObject(onHitFXTag);
			onHitFX.transform.position = transform.position;
			onHitFX.transform.rotation = Quaternion.identity;
			onHitFX.transform.parent = fxParent.transform;
			onHitFX.SetActive(true);


			StartCoroutine(SetProjectileInactive(gameObject, 0f));	
		}
	}

	IEnumerator SetProjectileInactive(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
    }


}
