using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottlecap : MonoBehaviour 
{
	[SerializeField] GameObject fizzFX;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			var fizz = Instantiate(fizzFX, transform.position, fizzFX.transform.rotation);
			gameObject.SetActive(false);
			Destroy(fizz, 2f);
			Destroy(gameObject, 2f);
		}
	}
}
