using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
	[SerializeField] GameObject fxParent;
	[SerializeField] GameObject rockProjectile;

	Rigidbody2D rockRB;

	bool rockEquipped = true;

	void Start() 
	{
		rockRB = rockProjectile.GetComponent<Rigidbody2D>();
	}
	

	void Update() 
	{
		ThrowRock();
	}

	void ThrowRock()
	{
		if (rockEquipped)
		{

			if(Input.GetMouseButtonDown(0))
			{
				Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
				cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);

				var spawnPos = new Vector2(transform.position.x, transform.position.y - .1f); 

				var rock = Instantiate(rockProjectile, spawnPos, Quaternion.identity);
				rock.transform.LookAt(cursorPos);
				float destroyDelay = 12f;
				Destroy(rock, destroyDelay);
			}
			else if (Input.GetButtonDown("Fire1"))
			{
				var rightAnologXThrow = Input.GetAxis("Right Analog X");
       		    var rightAnologYThrow = Input.GetAxis("Right Analog Y");

				Vector3 stickDirection = new Vector3();
				stickDirection.x =rightAnologXThrow;
				stickDirection.y = rightAnologYThrow;
				stickDirection.Normalize();
				
				var rock = Instantiate(rockProjectile, transform.position, Quaternion.identity);
				rock.transform.LookAt(transform.position + stickDirection);
				rock.transform.parent = fxParent.transform;
				float destroyDelay = 12f;
				Destroy(rock, destroyDelay);
			}
		}
	}
}
