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
	
	bool rockEquipped = true;


	void Start() 
	{
		
	}
	

	void Update() 
	{
		ThrowRock();		
	}

    IEnumerator SetProjectileInactive(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
    }

	void ThrowRock()
	{
		if (rockEquipped)
		{

			if(Input.GetMouseButtonDown(0))
            {
                // Find Cursor Coordinates
                Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                cursorPos = Camera.main.ScreenToWorldPoint(cursorPos);

                GameObject rock = ObjectPooler.SharedInstance.GetPooledObject("Player Projectile");
                var projSpawnPos = new Vector2(transform.position.x, transform.position.y - .2f);
                rock.transform.position = projSpawnPos;
                rock.transform.rotation = transform.rotation;
                rock.SetActive(true);             
                rock.transform.LookAt(cursorPos);
                rock.transform.parent = fxParent.transform;
                StartCoroutine(SetProjectileInactive(rock, 12f));    

                PlayAttackAnimation();           
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                GameObject rock = ObjectPooler.SharedInstance.GetPooledObject("Player Projectile");
                var projSpawnPos = new Vector2(transform.position.x, transform.position.y - .2f);
                rock.transform.position = projSpawnPos;
                rock.transform.rotation = transform.rotation;
                rock.SetActive(true);
             	rock.transform.parent = fxParent.transform;
                StartCoroutine(SetProjectileInactive(rock, 12f));

                PlayAttackAnimation();
                AimProjOnController(rock);       
            }
        }
	}

    void AimProjOnController(GameObject proj)
    {
        // Right Analog Stick
        var rightAnalogXThrow = Input.GetAxis("Right Analog X");
        var rightAnalogYThrow = Input.GetAxis("Right Analog Y");

        Vector3 rightStickDirection = new Vector3();
        rightStickDirection.x = rightAnalogXThrow;
        rightStickDirection.y = rightAnalogYThrow;
        rightStickDirection.Normalize();

        bool rightStickMovement = Mathf.Abs(rightAnalogXThrow) > Mathf.Epsilon || Mathf.Abs(rightAnalogYThrow) > Mathf.Epsilon;

        // Left Analog Stick
        var leftAnalogXThrow = Input.GetAxis("Horizontal");
        var leftAnalogYThrow = Input.GetAxis("Vertical");

        Vector3 leftStickDirection = new Vector3();
        leftStickDirection.x = leftAnalogXThrow;
        leftStickDirection.y = leftAnalogYThrow;
        leftStickDirection.Normalize();

        bool leftStickMovement = Mathf.Abs(leftAnalogXThrow) > Mathf.Epsilon || Mathf.Abs(leftAnalogYThrow) > Mathf.Epsilon;

        if (rightStickMovement)
        {
            proj.transform.LookAt(transform.position + rightStickDirection);
        }
        else if (leftStickMovement && !rightStickMovement)
        {
            proj.transform.LookAt(transform.position + leftStickDirection);
        }
        else if (!leftStickMovement && !rightStickMovement)
        {
            if (pitForwardMeshes.activeInHierarchy)
            {
                proj.transform.LookAt(transform.position + new Vector3(0, -1));
            }
            else if (pitForwardLRMeshes.activeInHierarchy)
            {
                if (pitForwardLR.transform.localScale.x == 1)
                {
                    proj.transform.LookAt(transform.position + new Vector3(-10, 0));
                }
                else if (pitForwardLR.transform.localScale.x == -1)
                {
                    proj.transform.LookAt(transform.position + new Vector3(10, 0));
                }
            }
            else if (pitBackMeshes.activeInHierarchy)
            {
                proj.transform.LookAt(transform.position + new Vector3(0, 1));
            }
            else if (pitBackLRMeshes)
            {
                if (pitBackLR.transform.localScale.x == 1)
                {
                    proj.transform.LookAt(transform.position + new Vector3(-1, 1));
                }
                else if (pitBackLR.transform.localScale.x == -1)
                {
                    proj.transform.LookAt(transform.position + new Vector3(1, 1));
                }
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
