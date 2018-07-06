using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour 
{
	[SerializeField] Texture2D reticleSprite;

	void Update() 
	{
		OnGUI();
		Cursor.visible = false;
	}

	void OnGUI()
	{
		// draw on current mouse position
		float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (reticleSprite.width / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (reticleSprite.height / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, reticleSprite.width, reticleSprite.height), reticleSprite);

	}
}
