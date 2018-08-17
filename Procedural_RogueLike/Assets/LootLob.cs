using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootLob : MonoBehaviour 
{
	[SerializeField] public float lobTime = 1f;
	[SerializeField] float minArc = 1f;
	[SerializeField] float maxArc = 3f;

	Vector3 endingPoint;
	GameObject core;

	Raycast ray;

	void Start() 
	{
		core = transform.GetChild(0).gameObject;	
		ray = GetComponent<Raycast>();
	}

	void OnEnable()
	{	
		Invoke("Lob", .01f);
	}

	void OnDisable()
	{
		core.transform.localPosition = Vector3.zero;
	}

	
	

    private void Lob()
    {
        float arc = Random.Range(minArc, maxArc);

        float xRandom = Random.Range(-1f, 1f);
        float yRandom = Random.Range(-1f, 1f);

		if (ray.wallUp)
		{
			yRandom = Random.Range(-1.25f, 0f);
		}
		if (ray.wallDown)
		{
			yRandom = Random.Range(0f, 1.25f);
		}
		if (ray.wallRight)
		{
			xRandom = Random.Range(-1.25f, 0f);
		}
		if (ray.wallLeft)
		{
			xRandom = Random.Range(0f, 1.25f);
		}

        endingPoint = new Vector3(transform.position.x + xRandom, transform.position.y + yRandom);
		
        iTween.MoveBy(core, iTween.Hash("y", arc, "time", lobTime / 2, "easeType", iTween.EaseType.easeOutQuad));
        iTween.MoveBy(core, iTween.Hash("y", -arc, "time", lobTime / 2, "delay", lobTime / 2, "easeType", iTween.EaseType.easeInCubic));
        iTween.MoveTo(gameObject, iTween.Hash("position", endingPoint, "time", lobTime, "easeType", iTween.EaseType.linear));		

		iTween.ScaleTo(gameObject, new Vector3(1.25f, 1.25f, 1.25f), lobTime / 2);
		iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", lobTime / 2, "delay", lobTime / 2));
    }
}
