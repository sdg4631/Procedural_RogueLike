using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour 
{
	BoxCollider2D myCollider;
	CameraShake cameraShake;

	void Start()
	{
		cameraShake = FindObjectOfType<CameraShake>();
		myCollider = GetComponent<BoxCollider2D>();
		myCollider.enabled = false;
		StartCoroutine(EnableBoxCollider());
	}

	IEnumerator EnableBoxCollider()
	{
		yield return new WaitForSeconds(.6f);
		myCollider.enabled = true;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			StartCoroutine(cameraShake.Shake(.1f, .1f, 0));
		}
	}
}
