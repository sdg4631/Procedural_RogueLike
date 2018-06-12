using System;

// Serializable so it will show up in the inspector.
[Serializable] 
public class IntRange 
{
	public int minValue; // the minimum value in this range
	public int maxValue;	// the maximum value in this range

	// Constructor to set the values
	public IntRange(int min, int max)
	{
		minValue = min;
		maxValue = max;
	}

	// Get a random value from the range
	public int Random
	{
		get { return UnityEngine.Random.Range(minValue, maxValue); }
	}


}
