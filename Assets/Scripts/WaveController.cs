using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{

	public static WaveController Instance { get; private set; }

	public Player playerObject;
	//public static Player player;

	public Enemy rammingEnemyPrefab;
	public Enemy shootingEnemyPrefab;
    public TMP_Text m_scoreText = null;
    public GameObject[] meteroidPrefabs;

	public bool isActive = true;

	public float minSpawnDistanceToPlayer = 10f;
	public float maxSpawnDistanceToPlayer = 20f;

	public float maxSpawnDelay = 5f;

	private int waveNumber = 1;
	private bool waveActive = false;
	private float activeWaveUptime = 0;

	[Header("Wave Calculation Function:")]

	public float factor = 0.05f;
	public float exponent = 3;
	public float summand = 1.2f;

	private GameObject[] activeEnemies;

	private int score;
	public int Score { get => score; private set { score = value; onScoreChange?.Invoke(); } }
	public void AddEnemyKillScore() 
    { 
        Score += 10;
        m_scoreText.text = Score.ToString();    
    }
	public void AddWaveScore(int waveNumber) 
    { 
        Score += 100 * waveNumber; 
        m_scoreText.text = Score.ToString();    
    }

	public delegate void OnScoreChange();
	public OnScoreChange onScoreChange;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

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

		// add points for last wave
		AddWaveScore(waveNumber - 1);

		int enemyCount = Mathf.RoundToInt(factor * Mathf.Pow(waveNumber, exponent) + summand);
        int meteroidCount = enemyCount;
		activeEnemies = new GameObject[enemyCount];

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

        // spawn all meteroids
        for (int i = 0; i < meteroidCount; i++)
		{
			// Select meteroid
			// ---------------
			GameObject meteroid = Instantiate(meteroidPrefabs.Random());
            meteroid.transform.position = GetRandomPositionAroundPlayer();
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

	public Vector3 GetRandomPositionAroundPlayer() {
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

		if (playerObject != null)
		{
			// create enemy
			GameObject enemyGO = Instantiate(enemyPrefab.gameObject, GetRandomPositionAroundPlayer(), enemyPrefab.transform.rotation);
			enemyGO.GetComponent<Enemy>().SetTarget(playerObject);

			// set enemy to current wave
			activeEnemies[arrayIndex] = enemyGO;
		}
	}
}
