using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour 
{
	Animator myAnimator;
	Animation anim;
	float timeBetweenReflections;

	Vector3 firstReflectionLinePos;
	Vector3 secondReflectionLinePos;

	void Start()
	{
		myAnimator = GetComponent<Animator>();
		StartCoroutine(PlayReflectAnimation());
	}

	void OnEnable()
	{
		myAnimator = GetComponent<Animator>();
		StartCoroutine(PlayReflectAnimation());

		// TODO figure out how to reset pos on disable
		firstReflectionLinePos = transform.GetChild(0).position;
		secondReflectionLinePos = transform.GetChild(1).position;
	}



	IEnumerator PlayReflectAnimation()
	{
		while (true)
		{
			myAnimator.SetTrigger("Reflect");

			timeBetweenReflections = Random.Range(1f, 6f);

			yield return new WaitForSeconds(timeBetweenReflections);

			myAnimator.ResetTrigger("Reflect");
		}	
	}
}
