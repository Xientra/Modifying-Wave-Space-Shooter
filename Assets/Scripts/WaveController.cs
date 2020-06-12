using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

	public GameObject playerObject;
	public static GameObject player;

	public Enemy enemy1Prefab;

	public bool isActive = true;

	public float minSpawnDistanceToPlayer = 10f;
	public float maxSpawnDistanceToPlayer = 20f;

	private int waveNumber = 1;
	private bool waveActive = false;

	private GameObject[] activeEnemies;

	private void Awake()
	{
		player = playerObject;
	}

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

			GameObject enemyGO = Instantiate(enemy1Prefab.gameObject, rndPos, enemy1Prefab.transform.rotation);

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
