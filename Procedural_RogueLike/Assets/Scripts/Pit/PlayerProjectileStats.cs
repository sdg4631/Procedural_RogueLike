using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileStats : MonoBehaviour 
{
	[SerializeField] public int projectileDamage;
	[SerializeField] public float projectileSpeed;
	[SerializeField] public float projectileForce;
	[SerializeField] public float killingBlowForce;
	[SerializeField] public float targetDrag;
}
