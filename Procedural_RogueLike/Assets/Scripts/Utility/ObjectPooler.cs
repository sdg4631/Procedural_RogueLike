using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
	public GameObject objectToPool;
	public int amountToPool;
	public bool shouldExpand = true;
}

public class ObjectPooler : MonoBehaviour 
{
	public static ObjectPooler SharedInstance;

	public List<GameObject> pooledObjects;	
	public List<ObjectPoolItem> itemsToPool;
	

	void Awake()
	{
		SharedInstance = this;
	}

	void Start()
	{
		// The for loop will instantiate the objectToPool GameObject the specified number of times in numberToPool. 
		// Then the GameObjects are set to an inactive state before adding them to the pooledObjects list.
		// The foreach loop iterates through all instances of ObjectPoolItem and add the appropriate objects to the object pool
		
		pooledObjects = new List<GameObject>();
		foreach (ObjectPoolItem item in itemsToPool)
		{
			for (int i = 0; i < item.amountToPool; i++)
			{
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive(false);
				obj.transform.parent = transform;
				pooledObjects.Add(obj);
			}
		}
		
	}

	public GameObject GetPooledObject(string tag)
	{
		// Iterates through our list of pooled objects and returns an inactive gameobject
		// in our hierarchy for the method that called it. 

		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
			{
				return pooledObjects[i];
			}
		}
		
		// If a player bullet is requested from the pool and no inactive ones can be found, 
		// this block checks to see it's possible to expand the pool instead of exiting the method. 
		// If so, you instantiate a new bullet, set it to inactive, 
		// add it to the pool and return it to the method that requested it.

		// GetPooledObject now takes a string parameter so your game can request an object by its tag. 
		// The method will search the object pool for an inactive object that has a matching tag, 
		// and then it returns an eligible object.

    	// Additionally, if it finds no appropriate object, it checks the relevant ObjectPoolItem 
		// instance by the tag to see if it's possible to expand it.

		foreach (ObjectPoolItem item in itemsToPool)
		{
			if (item.objectToPool.tag == tag)
			{
				if (item.shouldExpand)
				{
					GameObject obj = (GameObject)Instantiate(item.objectToPool);
					obj.SetActive(false);
					obj.transform.parent = transform;
					pooledObjects.Add(obj);
					return obj;
				}
			}
		}
		return null;		
	}
}
