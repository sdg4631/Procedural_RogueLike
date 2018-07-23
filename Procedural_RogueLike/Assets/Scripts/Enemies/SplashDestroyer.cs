using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ATTACH TO ENEMY SPLASH PARTICLE PREFAB
public class SplashDestroyer : MonoBehaviour 
{
	void OnEnable() 
	{
		Invoke("SetSplashInactive", 2f);
	}
	
	void SetSplashInactive()
    {
        this.gameObject.SetActive(false);
    }
}
