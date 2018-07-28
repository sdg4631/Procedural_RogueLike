using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour 
{
	// Config
	[SerializeField] float horizontalMoveSpeed = 5;
	[SerializeField] float verticalMoveSpeed = 4;

	// Cached Component References
	Rigidbody2D myRigidBody;

    // Sprite Parent GameObjects
    [SerializeField] GameObject pitRoot;
	[SerializeField] GameObject pitForward;
	[SerializeField] GameObject pitForwardLR;
    [SerializeField] GameObject pitBack;
    [SerializeField] GameObject pitBackLR;

    //Meshes
	[SerializeField] GameObject pitForwardMeshes;
	[SerializeField] GameObject pitForwardLRMeshes;
    [SerializeField] GameObject pitBackMeshes;
    [SerializeField] GameObject pitBackLRMeshes;

    // Eye Lights
    [SerializeField] GameObject pitFrontLeftEye;
    [SerializeField] GameObject pitFrontRightEye;
    [SerializeField] GameObject pitFrontLRLeftEye;
    [SerializeField] GameObject pitFrontLRRightEye;

    // Particles
    [SerializeField] GameObject fxParent;
    [SerializeField] GameObject pitFootstepFX;
    [SerializeField] GameObject dustTrailFX;
    [SerializeField] GameObject dashFXPrefab;

    // Cursor Variables
    bool aimingWithCursor = false;
    float cursorLastMovedTimer;
    Vector3 lastMouseCoordinate = Vector3.zero;

    // Controller Variables
    bool aimingWithController = false;

    bool playerIsMoving = false;
    Vector3 currentPos; 
    float footstepTimer = 0;
    float dustTimer = 0;

    float xThrow;
    float yThrow;


    // TODO remove public
	public DashState dashState;
    public float timeDashing;
    public float maxDashTime = 2f;
    public float dashSpeed = 5f;
    public float currentDashCooldown = 0f;
    public float maxDashCooldown = 3f;

    public Vector2 savedVelocity;
    float rotationZ;

	public enum DashState 
 	{
     Ready,
     Dashing,
     Cooldown
	}

    [SerializeField] public GameObject dashCooldownBar = null;
    public float fillAmount;

    CameraShake cameraShake;
    

	void Start() 
	{
		myRigidBody = GetComponent<Rigidbody2D>();
        cameraShake = FindObjectOfType<CameraShake>();      
        dashCooldownBar.SetActive(false); 
	}
	
	void Update()
    {
        
        CheckForCursorMovement();
        CheckForPlayerMovement();
        Dash();
        if(dashState != DashState.Dashing) { Move(); }
        ControlSpriteWithCursorAiming();
        AimingWithController();
        PlayMovementParticles();
        DashBar();
        SetEyes();
        
    }

    void SetEyes()
    {
        // Facing Forward
        pitFrontLeftEye.SetActive(pitForwardMeshes.activeInHierarchy);
        pitFrontRightEye.SetActive(pitForwardMeshes.activeInHierarchy);

        // Facing LR
        pitFrontLRLeftEye.SetActive(pitForwardLRMeshes.activeInHierarchy);
        pitFrontLRRightEye.SetActive(pitForwardLRMeshes.activeInHierarchy);
    }

    private void Dash()
    {
        // Player Input Returns
        xThrow = Input.GetAxisRaw("Horizontal");
        yThrow = Input.GetAxisRaw("Vertical");

        bool movementInput = (Mathf.Abs(xThrow) >= Mathf.Epsilon || Mathf.Abs(yThrow) >= Mathf.Epsilon);

       var dashDirection = new Vector3();
       dashDirection.x = xThrow;
       dashDirection.y = yThrow;
       dashDirection.Normalize();

        switch (dashState) 
        {
			case DashState.Ready:
				var isDashKeyDown = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Dash");
				if(isDashKeyDown && movementInput)
                {
                    savedVelocity = myRigidBody.velocity;
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), (LayerMask.NameToLayer("Enemy")), true);
                    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), (LayerMask.NameToLayer("Projectile")), true);
                    DashMovement(dashDirection);
                    DashParticles(dashDirection);
                    dashState = DashState.Dashing;
                }
                break;

			case DashState.Dashing:
                
				timeDashing += Time.deltaTime;
				if(timeDashing >= maxDashTime)
				{
					timeDashing = 0;
                    currentDashCooldown = 0f;
					myRigidBody.velocity = savedVelocity;
					dashState = DashState.Cooldown;
				}
			break;

			case DashState.Cooldown:
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), (LayerMask.NameToLayer("Enemy")), false);
                Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), (LayerMask.NameToLayer("Projectile")), false);
				currentDashCooldown += Time.deltaTime;
				if(currentDashCooldown >= maxDashCooldown)
				{					
					dashState = DashState.Ready;
				}
			break;
        }
    }

    void DashBar()
    {
        fillAmount = currentDashCooldown / maxDashCooldown;
        dashCooldownBar.GetComponentInChildren<Image>().fillAmount = fillAmount;

        DashEffects(fillAmount);
    }

    void DashEffects(float fillAmount)
    {
        if (fillAmount < 1)
        { 
            dashCooldownBar.GetComponentInChildren<Image>().color = new Color(220,224,0, 255);
            dashCooldownBar.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            dashCooldownBar.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            dashCooldownBar.SetActive(true);
        }
        else if (fillAmount >= 1) 
        {
            dashCooldownBar.GetComponentInChildren<Image>().color = new Color(0,229,0, 255);
            dashCooldownBar.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
            
            // For each image componenet in dashcooldownbar, fade the color transparency from 255 to 0
            Image[] images = dashCooldownBar.GetComponentsInChildren<Image>();
            foreach(var image in images)
            {
                image.CrossFadeAlpha(0, .1f, false);
                dashCooldownBar.GetComponent<RectTransform>().localPosition = new Vector3(0.07f, -.15f, 0f);
            }

            Invoke("SetBarInactive", .2f);
        }
    }

    void SetBarInactive()
    {
        dashCooldownBar.SetActive(false);
    }

    private void DashParticles(Vector3 dashDirection)
    {
        if (playerIsMoving)
        {
            var dashFX = Instantiate(dashFXPrefab, transform.position, Quaternion.identity);
            rotationZ = Mathf.Atan2(dashDirection.y, dashDirection.x) * Mathf.Rad2Deg;  
            dashFX.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            float destroyDelay = .7f;
            Destroy(dashFX, destroyDelay);
        }
        // else if (dashDirection.x == 0 && dashDirection.y == 0)
        // {
        //     if (pitForwardMeshes.activeInHierarchy)
        //     {
        //         rotationZ = Mathf.Atan2(-1, 0) * Mathf.Rad2Deg;
        //     }
        //     else if (pitForwardLRMeshes.activeInHierarchy)
        //     {
        //         if (pitForwardLR.transform.localScale.x == 1)
        //         {
        //            rotationZ = Mathf.Atan2(0, -1) * Mathf.Rad2Deg; 
        //         }
        //         else if (pitForwardLR.transform.localScale.x == -1)
        //         {
        //             rotationZ = Mathf.Atan2(0, 1) * Mathf.Rad2Deg; 
        //         }
        //     }
        //     else if (pitBackMeshes.activeInHierarchy)
        //     {
        //         rotationZ = Mathf.Atan2(1, 0) * Mathf.Rad2Deg;
        //     }
        //     else if (pitBackLRMeshes.activeInHierarchy)
        //     {
        //         if (pitBackLR.transform.localScale.x == 1)
        //         {
        //             rotationZ = Mathf.Atan2(1, -1) * Mathf.Rad2Deg;
        //         }
        //         else if (pitBackLR.transform.localScale.x == -1)
        //         {
        //             rotationZ = Mathf.Atan2(1, 1) * Mathf.Rad2Deg;
        //         } 
        //     }    
        // }
        
        
    }

    private void DashMovement(Vector3 dashDirection)
    {
        if (playerIsMoving)
        {
            myRigidBody.velocity = new Vector2(dashDirection.x * dashSpeed, dashDirection.y * dashSpeed);
        }
        // else if (!playerIsMoving)
        // {
        //     if (pitForwardMeshes.activeInHierarchy)
        //     {
        //         myRigidBody.velocity = new Vector2(0, -1 * dashSpeed);
        //     }
        //     else if (pitForwardLRMeshes.activeInHierarchy)
        //     {
        //         if (pitForwardLR.transform.localScale.x == 1)
        //         {
        //             myRigidBody.velocity = new Vector2(-1 * dashSpeed, 0);
        //         }
        //         else if (pitForwardLR.transform.localScale.x == -1)
        //         {
        //             myRigidBody.velocity = new Vector2(1 * dashSpeed, 0);
        //         }
        //     }
        //     else if (pitBackMeshes.activeInHierarchy)
        //     {
        //         myRigidBody.velocity = new Vector2(0, 1 * dashSpeed);
        //     }
        //     else if (pitBackLRMeshes.activeInHierarchy)
        //     {
        //         if (pitBackLR.transform.localScale.x == 1)
        //         {
        //             myRigidBody.velocity = new Vector2(-1 * dashSpeed, 1 * dashSpeed);
        //         }
        //         else if (pitBackLR.transform.localScale.x == -1)
        //         {
        //             myRigidBody.velocity = new Vector2(1 * dashSpeed, 1 * dashSpeed);
        //         }
        //     }
        // }
    }

    private void CheckForPlayerMovement()
    {
        if (currentPos != transform.position)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
        currentPos = transform.position;
    }

    private void AimingWithController()
    {
        var rightAnalogXThrow = Input.GetAxis("Right Analog X");
        var rightAnalogYThrow = Input.GetAxis("Right Analog Y");

        if (rightAnalogXThrow == 0 && rightAnalogYThrow == 0)
        {
            aimingWithController = false;
        }
        else
        {
            aimingWithController = true;
        }

        bool aimingForwardWithController = rightAnalogXThrow >= -0.25 && rightAnalogXThrow <= 0.25 && rightAnalogYThrow < 0;
        bool aimingLeftWithController = rightAnalogXThrow < 0 && rightAnalogYThrow < 0.5;
        bool aimingRightWithController = rightAnalogXThrow > 0 && rightAnalogYThrow < 0.5;
        bool aimingBackWithController = rightAnalogXThrow >= -0.25 && rightAnalogXThrow <= 0.25 && rightAnalogYThrow > 0;
        bool aimingBackLeftWithController = rightAnalogXThrow < 0 && rightAnalogYThrow >= 0.5;
        bool aimingBackRightWithController = rightAnalogXThrow > 0 && rightAnalogYThrow >= 0.5;

        // Activate/Deactivate Parent GameObject

        if (aimingForwardWithController)
        {
            pitForwardMeshes.SetActive(true);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingLeftWithController || aimingRightWithController)
        {
            pitForwardLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingBackWithController)
        {
            pitBackMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (aimingBackLeftWithController || aimingBackRightWithController)
        {
            pitBackLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
        }

        if (aimingLeftWithController || aimingBackLeftWithController)
        {
            pitForwardLR.transform.localScale = new Vector2(1, 1f);
            pitBackLR.transform.localScale = new Vector2(1, 1f);
        }


        if (aimingRightWithController || aimingBackRightWithController)
        {
            pitForwardLR.transform.localScale = new Vector2(-1, 1f);
            pitBackLR.transform.localScale = new Vector2(-1, 1f);
        }

        // Set Animation States
        if (pitForwardMeshes.activeInHierarchy)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLRMeshes.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBackMeshes.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLRMeshes.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
        }
    }

    void CheckForCursorMovement()
    {
        if (Input.mousePosition != lastMouseCoordinate || Input.GetMouseButtonDown(0)) // Moving Cursor or Attacking
        { 
            cursorLastMovedTimer = 0.0f;
            aimingWithCursor = true;
        }

        if (Input.mousePosition == lastMouseCoordinate) // Not Moving Cursor
        { 
            cursorLastMovedTimer += Time.deltaTime;
        }

        if (cursorLastMovedTimer >= .5f)
        {
            aimingWithCursor = false;
        }
        lastMouseCoordinate = Input.mousePosition;
    }

    void Move()
    {
        // Player Input Returns
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // X Movement 
        var xVelocity = new Vector2(xThrow * horizontalMoveSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = xVelocity;

        // Y Movement 
        var yVelocity = new Vector2(myRigidBody.velocity.x, yThrow * verticalMoveSpeed);
        myRigidBody.velocity = yVelocity;

          
        bool playerNotMovingWithInput = xThrow == 0 && yThrow == 0;
        if (playerNotMovingWithInput || !playerIsMoving)
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", false);
        }
        else
        {
            pitRoot.GetComponent<Animator>().SetBool("isRunning", true);
        }
        currentPos = transform.position;  

        if (aimingWithCursor == false && aimingWithController == false)
        {
            ControlSpriteWithMoving(xThrow, yThrow);
		    FlipSpriteWhileMoving();
        }
    }

    void ControlSpriteWithMoving(float xThrow, float yThrow)
    {    
        bool playerMovingForward = xThrow >= -0.25 && xThrow <= 0.25 && yThrow < 0;
        bool playerMovingLeft = xThrow < 0 && yThrow < 0.5;
        bool playerMovingRight = xThrow > 0 && yThrow < 0.5;
        bool playerMovingBack = xThrow >= -0.25 && xThrow <= 0.25 && yThrow > 0;
        bool playerMovingBackLeft = xThrow < 0 && yThrow >= 0.5;
        bool playerMovingBackRight = xThrow > 0 && yThrow >= 0.5;

        // Activate/Deactivate Parent GameObject
        if (playerMovingForward)
        {
            pitForwardMeshes.SetActive(true);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
            
        }
        else if (playerMovingLeft || playerMovingRight)
        {
            pitForwardLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (playerMovingBack)
        {
            pitBackMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackLRMeshes.SetActive(false);
        }
        else if (playerMovingBackLeft || playerMovingBackRight)
        {
            pitBackLRMeshes.SetActive(true);
            pitForwardMeshes.SetActive(false);
            pitForwardLRMeshes.SetActive(false);
            pitBackMeshes.SetActive(false);
        }

        // Set Animation States
        if (pitForwardMeshes.activeInHierarchy)
        {
            pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
        }
        else if (pitForwardLRMeshes.activeInHierarchy)
        {
            pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
        }
        else if (pitBackMeshes.activeInHierarchy)
        {
            pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
        }
        else if (pitBackLRMeshes.activeInHierarchy)
        {
            pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
        }
    }

    void FlipSpriteWhileMoving()
	{
		bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
		if (playerHasHorizontalSpeed)
		{
			pitForwardLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
            pitBackLR.transform.localScale = new Vector2(-Mathf.Sign(myRigidBody.velocity.x), 1f);
		}
	}

    void ControlSpriteWithCursorAiming()
    {
        Vector3 cursor = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        cursor = Camera.main.ScreenToWorldPoint(cursor);

        float angle = Mathf.Atan2(cursor.y - transform.position.y, cursor.x - transform.position.x) * Mathf.Rad2Deg;

        var facingForward = angle >= -135 && angle <= -45;
        var facingForwardLR = ((angle < -135 && angle > -180) || (angle < 180 && angle > 145)) || ((angle < 0 && angle > -45) || (angle >= 0 && angle < 35));
        var facingBack = angle <= 105 && angle >= 75;
        var facingBackLR = (angle <= 135 && angle > 105) || (angle < 75 && angle >= 45);

        if (aimingWithCursor == true) // TODO or firing???
        {
            // Activate/Deactivate Parent GameObject
            if (facingForward)
            {
                pitForwardMeshes.SetActive(true);
                pitForwardLRMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);

            }
            else if (facingForwardLR)
            {
                pitForwardLRMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);
            }
            else if (facingBack)
            {
                pitBackMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitForwardLRMeshes.SetActive(false);
                pitBackLRMeshes.SetActive(false);
            }
            else if (facingBackLR)
            {
                pitBackLRMeshes.SetActive(true);
                pitForwardMeshes.SetActive(false);
                pitForwardLRMeshes.SetActive(false);
                pitBackMeshes.SetActive(false);
            }

            // Set Animation States
            if (pitForwardMeshes.activeInHierarchy)
            {
                pitForward.GetComponent<Animator>().SetBool("FrontFeet", playerIsMoving);
            }
            else if (pitForwardLRMeshes.activeInHierarchy)
            {
                pitForwardLR.GetComponent<Animator>().SetBool("FrontLRFeet", playerIsMoving);
            }
            else if (pitBackMeshes.activeInHierarchy)
            {
                pitBack.GetComponent<Animator>().SetBool("BackFeet", playerIsMoving);
            }
            else if (pitBackLRMeshes.activeInHierarchy)
            {
                pitBackLR.GetComponent<Animator>().SetBool("BackLRFeet", playerIsMoving);
            }

            FlipSpriteWhileAiming(angle);
        } 
    }
        
    void FlipSpriteWhileAiming(float angle)
    {
        var spriteFacingLeft = ((angle < -135 && angle > -180) || (angle < 180 && angle > 145)) || (angle <= 135 && angle > 95);
        var spriteFacingRight = ((angle < 0 && angle > -45) || (angle >= 0 && angle < 35)) || (angle < 85 && angle >= 45);

        if (spriteFacingLeft)
        {
            pitForwardLR.transform.localScale = new Vector2(1, 1f);
            pitBackLR.transform.localScale = new Vector2(1, 1f);
        }


        if (spriteFacingRight)
        {
            pitForwardLR.transform.localScale = new Vector2(-1, 1f);
            pitBackLR.transform.localScale = new Vector2(-1, 1f);
        }
    }

    void PlayMovementParticles()
    {
        // Footsteps
        if (playerIsMoving && footstepTimer <= 0)
        {   
            var footstepSpawnPosition = new Vector2(transform.position.x, transform.position.y - .5f);       
            var footsteps = Instantiate(pitFootstepFX, footstepSpawnPosition, Quaternion.identity);
            footsteps.transform.parent = fxParent.transform;
            Destroy(footsteps, 1f);
            footstepTimer = .2f;           
        }
        footstepTimer -= Time.deltaTime;

        // Dust Trail
        if (playerIsMoving && dustTimer <= 0)
        {
            var dustTrailSpawnPosition = new Vector2(transform.position.x, transform.position.y - .5f);
            var dustTrail = Instantiate(dustTrailFX, dustTrailSpawnPosition, Quaternion.identity);
            dustTrail.transform.parent = fxParent.transform;
            float destroyTimer = 3f;
            Destroy(dustTrail, destroyTimer);
            dustTimer = 0.02f;
        }
        dustTimer -= Time.deltaTime;
    }

}
