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
		if (playerObject == null)
			isActive = false;

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
		int enemyCount = 10 * waveNumber;

		activeEnemies = new GameObject[10 * waveNumber];

		int rammingEnemyCount = Random.Range(0, enemyCount + 1);

		int arrIndex = 0;

		// spawn all ramming enemies
		for (int i = 0; i < rammingEnemyCount; i++)
		{
			// create enemy
			GameObject enemyGO = Instantiate(rammingEnemyPrefab.gameObject, GetRandomPositionAroundPlayer(), rammingEnemyPrefab.transform.rotation);
			enemyGO.GetComponent<Enemy>().SetTarget(playerObject);

			// set enemy to current wave
			activeEnemies[arrIndex] = enemyGO;
			arrIndex++;
		}

		// spawn all shooting enemies
		for (int i = 0; i < enemyCount - rammingEnemyCount; i++)
		{
			// create enemy
			GameObject enemyGO = Instantiate(shootingEnemyPrefab.gameObject, GetRandomPositionAroundPlayer(), shootingEnemyPrefab.transform.rotation);
			enemyGO.GetComponent<Enemy>().SetTarget(playerObject);

			// set enemy to current wave
			activeEnemies[i] = enemyGO;
			arrIndex++;
		}

		waveNumber++;
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

	private Vector3 GetRandomPositionAroundPlayer() {
		float rndDistance = Random.Range(minSpawnDistanceToPlayer, maxSpawnDistanceToPlayer);
		float rndRotation = Random.Range(0f, 360f);

		// get randomly rotated vector
		Vector3 rndPos = Quaternion.AngleAxis(rndRotation, Vector3.up) * Vector3.forward;

		// scale vector
		rndPos *= rndDistance;

		// rndPos relative to Player
		rndPos += playerObject.transform.position;

		return rndPos;
	}
}
