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
}
