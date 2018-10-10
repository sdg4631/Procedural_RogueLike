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

	GameObject fxParent;

    public string currentProj;
    public string currentMuzzle;

    float attackTimer;
    [SerializeField] public float minTimeBetweenAttacks = 1f;

    Rigidbody2D myRigidbody;
    


    CameraShake cameraShake;
	

	void Start() 
	{
		currentProj = "FireballProjectile";
        currentMuzzle = "FireballMuzzle";

        myRigidbody = GetComponent<Rigidbody2D>();
        cameraShake = FindObjectOfType<CameraShake>();

        attackTimer = minTimeBetweenAttacks;

        fxParent = GameObject.FindGameObjectWithTag("FXParent");
	}
	

	void Update() 
	{
		ThrowProjectile(currentProj);		
	}

    IEnumerator SetProjectileInactive(GameObject proj, float delay)
    {
        yield return new WaitForSeconds(delay);
        proj.SetActive(false);
    }

	void ThrowProjectile(string projectileTag)
	{
        attackTimer += Time.deltaTime;
      
        if (attackTimer > minTimeBetweenAttacks)
        {
            if(Input.GetMouseButtonDown(0))
            {
                attackTimer = 0f;

                Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                var direction = (mousePosition - transform.position).normalized;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                GameObject proj = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
                var projSpawnPos = new Vector2(transform.position.x, transform.position.y + .3f);
                proj.transform.position = projSpawnPos;
                proj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                proj.SetActive(true);             
                proj.GetComponent<Rigidbody2D>().AddForce(proj.transform.right * proj.GetComponent<PlayerProjectileStats>().projectileSpeed);
                
                PlayAttackAnimation();   
                EnableMuzzleFlash(currentMuzzle, angle);        
                // StartCoroutine(cameraShake.Shake(.1f, .05f, 0));
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                attackTimer = 0f;

                GameObject proj = ObjectPooler.SharedInstance.GetPooledObject(projectileTag);
                var projSpawnPos = new Vector2(transform.position.x, transform.position.y - .2f);
                proj.transform.position = projSpawnPos;
                proj.transform.rotation = transform.rotation;
                proj.SetActive(true);
                proj.transform.parent = fxParent.transform;
                StartCoroutine(SetProjectileInactive(proj, 12f));

                PlayAttackAnimation();
                AimProjOnController(proj);       
            }
        }
	}

    void EnableMuzzleFlash(string muzzleTag, float angle)
    {
        GameObject muzzleFlash = ObjectPooler.SharedInstance.GetPooledObject(muzzleTag);
        var projSpawnPos = new Vector2(transform.position.x, transform.position.y + .3f);
        muzzleFlash.transform.position = projSpawnPos;
        muzzleFlash.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        muzzleFlash.SetActive(true);
        StartCoroutine(SetProjectileInactive(muzzleFlash, 2f));
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
