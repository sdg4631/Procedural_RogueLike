using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour 
{
	[SerializeField] Text bottlecapUI;
	public int numOfBottlecaps = 0;

	void Start() 
	{
		
	}
	

	void Update() 
	{
		bottlecapUI.text = numOfBottlecaps.ToString();
	}
}
