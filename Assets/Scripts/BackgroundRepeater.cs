using UnityEngine;

public class BackgroundRepeater : MonoBehaviour
{
	public Transform player;
	public GameObject backgroundTilePrefab;
	public Vector2 tileSize;

	private GameObject[] tiles;
	private Vector2 halfSize;

	private void Start()
	{
		halfSize = new Vector2(tileSize.x / 2, tileSize.y / 2);
		tiles = new GameObject[4];
		int i = 0;
		for (int y = -1; y <= 1; y+=2)
		{
			for (int x = -1; x <= 1; x+=2)
			{
				tiles[i] = Instantiate(backgroundTilePrefab, new Vector3(x * halfSize.x, 0, y * halfSize.y), Quaternion.identity, transform);
				i++;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		int i = 0;
		for (int y = -1; y <= 1; y += 2)
		{
			for (int x = -1; x <= 1; x += 2)
			{
				UpdateTile(tiles[i], x, y);
				i++;
			}
		}
	}

	private void UpdateTile(GameObject go, int xSign, int ySign)
	{
		float xIndex = Mathf.Floor((player.position.x + xSign * halfSize.x) / tileSize.x);
		float yIndex = Mathf.Floor((player.position.z + ySign * halfSize.y) / tileSize.y);
		xIndex = xIndex * tileSize.x + halfSize.x;
		yIndex = yIndex * tileSize.y + halfSize.y;
		Vector3 pos = go.transform.position;
		pos.x = xIndex;
		pos.z = yIndex;
		go.transform.position = pos;
	}
}
