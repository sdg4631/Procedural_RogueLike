using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour 
{
	Animator myAnimator;
	float timeBetweenReflections;
	float rotationSpeed; 
	float rotationTimer;
	float rotationDuration;
	GameObject root;

	LootLob lootLob;

	void Start()
	{
		myAnimator = GetComponent<Animator>();	
		lootLob = transform.parent.GetComponentInParent<LootLob>();

		StartCoroutine(PlayReflectAnimation());

		root = transform.parent.parent.gameObject;

		rotationDuration = lootLob.lobTime;
		rotationTimer = 0f;
	}

	void Update()
    {
        RotationOnStart();
    }

    private void RotationOnStart()
    {
        rotationSpeed = Random.Range(900f, 2000f);
        rotationTimer += Time.deltaTime;
        if (rotationTimer < rotationDuration)
        {
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			// TODO Instantiate particles
			
			Destroy(root);
		}
	}
}
