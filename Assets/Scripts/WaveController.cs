using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

	public Player playerObject;
	//public static Player player;

	public Enemy rammingEnemyPrefab;
	public Enemy shootingEnemyPrefab;

	public bool isActive = true;

	public float minSpawnDistanceToPlayer = 10f;
	public float maxSpawnDistanceToPlayer = 20f;

	private int waveNumber = 1;
	private bool waveActive = false;

	private GameObject[] activeEnemies;

	void Start()
	{
		
	}

	void Update()
	{
		if (isActive)
		{

			if (waveActive == false)
			{
				SpawnWave();
				waveActive = true;
			}
			else
			{
				if (NoActiveEnemies())
					waveActive = false;
			}
		}
	}

	private void SpawnWave()
	{
		activeEnemies = new GameObject[10 * waveNumber];

		for (int i = 0; i < 10 * waveNumber; i++)
		{
			float rndDistance = Random.Range(minSpawnDistanceToPlayer, maxSpawnDistanceToPlayer);
			float rndRotation = Random.Range(0f, 360f);

			// get randomly rotated vector
			Vector3 rndPos = Quaternion.AngleAxis(rndRotation, Vector3.up) * Vector3.forward;

			// scale vector
			rndPos *= rndDistance;

			// create enemy
			GameObject enemyGO = Instantiate(shootingEnemyPrefab.gameObject, rndPos, shootingEnemyPrefab.transform.rotation);
			//GameObject enemyGO = Instantiate(rammingEnemyPrefab.gameObject, rndPos, rammingEnemyPrefab.transform.rotation);
			enemyGO.GetComponent<Enemy>().SetTarget(playerObject);

			// set enemy to current wave
			activeEnemies[i] = enemyGO;
		}
	}

	private bool NoActiveEnemies()
	{
		for (int i = 0; i < activeEnemies.Length; i++)
		{
			if (activeEnemies[i] != null)
				return false;
		}

		return true;
	}
}
