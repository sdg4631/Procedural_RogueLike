using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour 
{

	public bool enemiesCleared = false;
	[SerializeField] bool isEntryRoom = false;

	private EnemyTemplates templates;
	private GameObject spawnFirstTier;
	private GameObject spawnSecondTier;

	private List <GameObject> spawnedFirstTierEnemies = new List<GameObject>();
	private List <GameObject> spawnedSecondTierEnemies = new List<GameObject>();

	void Start() 
	{
		templates = GameObject.FindGameObjectWithTag("EnemyTemplates").GetComponent<EnemyTemplates>();

		float spawnDelay = 7f;
		Invoke("Spawn", spawnDelay);
	}

	void Update()
	{
		RemoveEnemiesFromListWhenDead();
		CheckForEnemiesInRoom();
	}

    private void CheckForEnemiesInRoom()
    {
        if (spawnedFirstTierEnemies.Count == 0 && spawnedSecondTierEnemies.Count == 0 || isEntryRoom == true)
        {
            enemiesCleared = true;
        }
        else
        {
            enemiesCleared = false;
        }
    }

    private void RemoveEnemiesFromListWhenDead()
    {
        foreach (var firstTierEnemy in spawnedFirstTierEnemies.ToArray())
        {
            if (firstTierEnemy == null)
            {
                spawnedFirstTierEnemies.Remove(firstTierEnemy);
            }
        }

        foreach (var secondTierEnemy in spawnedSecondTierEnemies.ToArray())
        {
            if (secondTierEnemy == null)
            {
                spawnedSecondTierEnemies.Remove(secondTierEnemy);
            }
        }
    }

    private void Spawn() // TODO Make this a for loop to iterate through all enemies spawned and add them to the list
	{
		if (isEntryRoom) { return; }

		var spawnPos = transform.position + new Vector3(Random.Range(-7, 7), Random.Range(-2, 2));

		var randomFirstTier = Random.Range(0, templates.firstTierEnemies.Length);
		spawnFirstTier = Instantiate(templates.firstTierEnemies[randomFirstTier], spawnPos, Quaternion.identity);
		spawnedFirstTierEnemies.Add(spawnFirstTier);

		var randomSecondTier = Random.Range(0, templates.secondTierEnemies.Length);
		spawnSecondTier = Instantiate(templates.secondTierEnemies[randomSecondTier], spawnPos, Quaternion.identity);
		spawnedSecondTierEnemies.Add(spawnSecondTier);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (isEntryRoom) { return; }
		
		if (other.gameObject.tag == "Player")
		{
			foreach(var firstTierEnemy in spawnedFirstTierEnemies)
			{
				firstTierEnemy.GetComponent<Animator>().enabled = true;
			}
			
			foreach(var secondTierEnemy in spawnedSecondTierEnemies)
			{
				secondTierEnemy.GetComponent<Animator>().enabled = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (isEntryRoom) { return; }
		
		if (other.gameObject.tag == "Player")
		{
			foreach(var firstTierEnemy in spawnedFirstTierEnemies)
			{
				firstTierEnemy.GetComponent<Animator>().enabled = false;
				firstTierEnemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			}
			
			foreach(var secondTierEnemy in spawnedSecondTierEnemies)
			{
				secondTierEnemy.GetComponent<Animator>().enabled = false;
				secondTierEnemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
			}
		}
	}
}
