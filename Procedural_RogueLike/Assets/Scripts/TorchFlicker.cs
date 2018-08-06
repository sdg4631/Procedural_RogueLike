using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlicker : MonoBehaviour 
{
	[SerializeField] float minRange = .25f;
	[SerializeField] float maxRange = .3f;

	[SerializeField] float minTimeBetweenFlicker = 1f;
	[SerializeField] float maxTimeBetweenFlicker = 2f;
	[SerializeField] float flickerTimer = 0f;

	void Start() 
	{
		
	}
	

	void Update() 
	{
		float random = Random.Range(minRange, maxRange);
		float timeBetweenFlicker = Random.Range(minTimeBetweenFlicker, maxTimeBetweenFlicker);

		flickerTimer += Time.deltaTime;
		if (flickerTimer >= timeBetweenFlicker)
		{
			transform.localScale = new Vector3(random, random);
			flickerTimer = 0f;
		}
	}
}
