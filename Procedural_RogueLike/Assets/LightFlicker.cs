using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour 
{
	Light2D myLight;

	[SerializeField] float minRange = 1.25f;
	[SerializeField] float maxRange = 1.75f;

	[SerializeField] float minTimeBetweenFlicker = 1f;
	[SerializeField] float maxTimeBetweenFlicker = 2f;
	[SerializeField] float flickerTimer = 0f;

	void Start() 
	{
		myLight = GetComponent<Light2D>();
	}
	

	void Update() 
	{
		float random = Random.Range(minRange, maxRange);
		float timeBetweenFlicker = Random.Range(minTimeBetweenFlicker, maxTimeBetweenFlicker);

		flickerTimer += Time.deltaTime;
		if (flickerTimer >= timeBetweenFlicker)
		{
			myLight.Range = random;
			flickerTimer = 0f;
		}
		
	}
}
