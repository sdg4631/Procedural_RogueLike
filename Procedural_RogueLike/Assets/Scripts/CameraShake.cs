using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{

	public IEnumerator Shake(float duration, float magnitude, float waitTime)
	{
		Vector3 originalPos = transform.localPosition;

		float elapsed = 0.0f; 
		
		yield return new WaitForSeconds(waitTime);

		while (elapsed < duration)
		{
			float x = Random.Range(-1f, 1f) * magnitude;
			float y = Random.Range(-1f, 1f) * magnitude;
			
			transform.localPosition = new Vector3(x, y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = originalPos;
	}
}
