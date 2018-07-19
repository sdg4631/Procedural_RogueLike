﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // // needed to make Serializable without monobehavior attached

[Serializable]
public class Stat 
{
    // [SerializeField] private BarScript bar;
    [SerializeField] private float maxVal;
    [SerializeField] private float currentVal;

    public float CurrentVal
    {
        get
        {
            return currentVal;
        }

        set
        {
            currentVal = Mathf.Clamp(value, 0, MaxVal); // clamp so that health can't exceed maxVal set in inspector
            // bar.Value = currentVal;
        }
    }

    public float MaxVal
    {
        get
        {
            return maxVal;
        }

        set
        {
            maxVal = value;
            // bar.MaxValue = maxVal;
        }
    }

    // maxVal and currentVal set in the inspector
    public void Initialize()
    {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}

