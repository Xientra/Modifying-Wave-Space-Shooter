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

	public float maxSpawnDelay = 5f;

	private int waveNumber = 1;
	private bool waveActive = false;
	private float activeWaveUptime = 0;

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
			}
			else
			{
				activeWaveUptime += Time.deltaTime;
				if (NoActiveEnemies() && activeWaveUptime > maxSpawnDelay)
				{
					waveActive = false;
					activeWaveUptime = 0;
				}
			}
		}
	}

    /*=============================*\
    |*   Public Member Functions   *|
    \*=============================*/

        /*============*\
        |*   Getter   *|
        \*============*/

        public GameObject[] GetActiveEnemies() { return activeEnemies; }

    private void SpawnWave()
	{
		waveActive = true;

		int enemyCount = 10 * waveNumber;

		activeEnemies = new GameObject[10 * waveNumber];

		int rammingEnemyCount = Random.Range(0, enemyCount + 1);

		int arrIndex = 0;

		// spawn all ramming enemies
		for (int i = 0; i < rammingEnemyCount; i++)
		{
			StartCoroutine(SpawnEnemy(rammingEnemyPrefab, Random.Range(0f, maxSpawnDelay), arrIndex));
			arrIndex++;
		}

		// spawn all shooting enemies
		for (int i = 0; i < enemyCount - rammingEnemyCount; i++)
		{
			StartCoroutine(SpawnEnemy(shootingEnemyPrefab, Random.Range(0f, maxSpawnDelay), arrIndex));
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

	private IEnumerator SpawnEnemy(Enemy enemyPrefab, float delay, int arrayIndex) {

		yield return new WaitForSeconds(delay);

		// create enemy
		GameObject enemyGO = Instantiate(enemyPrefab.gameObject, GetRandomPositionAroundPlayer(), enemyPrefab.transform.rotation);
		enemyGO.GetComponent<Enemy>().SetTarget(playerObject);

		// set enemy to current wave
		activeEnemies[arrayIndex] = enemyGO;
	}
}
