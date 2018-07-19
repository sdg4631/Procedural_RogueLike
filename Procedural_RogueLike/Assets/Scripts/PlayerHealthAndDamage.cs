using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAndDamage : MonoBehaviour 
{
	[SerializeField] public Stat health;
	CameraShake cameraShake;

	[SerializeField] private bool isInvulnerable = false;
	[SerializeField] private float invulnerabilityTimer = 2f;
	[SerializeField] private float invulnerabilityDuration = 2f;

	[SerializeField] GameObject pitFront;
	[SerializeField] GameObject pitFrontLR;

	private SkinnedMeshRenderer[] sprites = null;

	
	public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public bool startBlinking = false;


	void Awake()
	{
		health.Initialize();
	}

	void Start() 
	{
		cameraShake = FindObjectOfType<CameraShake>();
	}
	

	void Update() 
	{
		InvulnerabilityTimer();
		if (startBlinking == true) { StartBlinkingEffect(); }

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (!isInvulnerable)
            {
                isInvulnerable = true;
                invulnerabilityTimer = 0f;
                StartCoroutine(cameraShake.Shake(.2f, .2f, 0));
                health.CurrentVal--;

				// Animation and Blinking
                pitFront.GetComponent<Animator>().SetTrigger("Flinch");
				pitFrontLR.GetComponent<Animator>().SetTrigger("FlinchLR");
				startBlinking = true;
            }
		}
	}

    private void InvulnerabilityTimer()
    {
        invulnerabilityTimer += Time.deltaTime; 

		 if (invulnerabilityTimer > invulnerabilityDuration)
		{
			isInvulnerable = false;
		}
    }

	private void StartBlinkingEffect()
    {
		sprites = GetComponentsInChildren<SkinnedMeshRenderer>(includeInactive: true);

        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= invulnerabilityDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            foreach (var sprite in sprites)
			{
				sprite.enabled = true;
			}
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
			foreach (var sprite in sprites)
			{
				if (sprite.enabled == true)
				{
					sprite.enabled = false;
				}
				else
				{
					sprite.enabled = true;
				}
			}
        }
    }
}
