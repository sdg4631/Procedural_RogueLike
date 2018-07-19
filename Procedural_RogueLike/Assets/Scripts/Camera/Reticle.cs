using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour 
{
	[SerializeField] Texture2D crosshairTexture;
	[SerializeField] float crosshairScale = 1;

	void Update() 
	{
		Cursor.visible = false;
	}

	void OnGUI()
	{
		// draw on current mouse position
		float xMin = Screen.width - (Screen.width - Input.mousePosition.x) - (crosshairTexture.width * crosshairScale / 2);
		float yMin = (Screen.height - Input.mousePosition.y) - (crosshairTexture.height * crosshairScale / 2);
		GUI.DrawTexture(new Rect(xMin, yMin, crosshairTexture.width * crosshairScale, crosshairTexture.height * crosshairScale), crosshairTexture);

	}
}
