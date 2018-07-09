using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour 
{
	// Sprite Parent GameObjects
	[SerializeField] GameObject pitForward;
	[SerializeField] GameObject pitForwardLR;
    [SerializeField] GameObject pitBack;
    [SerializeField] GameObject pitBackLR;

    //Meshes
	[SerializeField] GameObject pitForwardMeshes;
	[SerializeField] GameObject pitForwardLRMeshes;
    [SerializeField] GameObject pitBackMeshes;
    [SerializeField] GameObject pitBackLRMeshes;

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
                PlayAttackAnimation();

                Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);

                var spawnPos = new Vector2(transform.position.x, transform.position.y - .1f);

                var rock = Instantiate(rockProjectile, spawnPos, Quaternion.identity);
                rock.transform.LookAt(cursorPos);
                rock.transform.parent = fxParent.transform;
                float destroyDelay = 12f;
                Destroy(rock, destroyDelay);
            }
            else if (Input.GetButtonDown("Fire1"))
			{
				PlayAttackAnimation();
				
				// Right Analog Stick
				var rightAnalogXThrow = Input.GetAxis("Right Analog X");
       		    var rightAnalogYThrow = Input.GetAxis("Right Analog Y");

				Vector3 rightStickDirection = new Vector3();
				rightStickDirection.x =rightAnalogXThrow;
				rightStickDirection.y = rightAnalogYThrow;
				rightStickDirection.Normalize();

				// Left Analog Stick
				var leftAnalogXThrow = Input.GetAxis("Horizontal");
        		var leftAnalogYThrow = Input.GetAxis("Vertical");

				Vector3 leftStickDirection = new Vector3();
				leftStickDirection.x =leftAnalogXThrow;
				leftStickDirection.y = leftAnalogYThrow;
				leftStickDirection.Normalize();
				
				var rock = Instantiate(rockProjectile, transform.position, Quaternion.identity);
				if (Mathf.Abs(rightAnalogXThrow) > Mathf.Epsilon || Mathf.Abs(rightAnalogYThrow) > Mathf.Epsilon)
				{
					rock.transform.LookAt(transform.position + rightStickDirection);
				}
				else
				{
					rock.transform.LookAt(transform.position + leftStickDirection);
				}
				
				rock.transform.parent = fxParent.transform;
				float destroyDelay = 12f;
				Destroy(rock, destroyDelay);
			}
		}
	}

    void PlayAttackAnimation()
    {
        if (pitForwardMeshes.activeInHierarchy)
        {
            pitForward.GetComponent<Animator>().Play("Front Attack", -1, 0);
        }
        else if (pitForwardLRMeshes.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().Play("Front LR Attack", -1, 0);
        }
        else if (pitBackMeshes.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().Play("Back Attack", -1, 0);
        }
        else if (pitBackLRMeshes.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().Play("Back LR Attack", -1, 0);
        }
    }
}
