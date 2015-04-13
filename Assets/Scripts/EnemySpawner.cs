using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	private GameObject playerPrefab;
	public GameObject enemy1Prefab;
	public GameObject enemy2Prefab;
	public GameObject enemy3Prefab;
	public GameObject enemy4Prefab;
	public float spawnsPerMinute;
	public float totalSpawns;
	private float currentSpawns = 0;
	public int maxSpawnPointX;
	public int maxSpawnPointY;
	public int minSpawnPointX;
	public int minSpawnPointY;
	private GameObject currentPrefab;

	void Start() {
		//InvokeRepeating ("Spawn", 5, 60 / spawnsPerMinute);
		playerPrefab = GameObject.Find ("Player").gameObject;
		currentPrefab = enemy1Prefab;
		SetLevel (Globals.Levels [1]);
	}

	void Reset() {
		currentSpawns = 0;
		totalSpawns = 0;
	}

	void SetLevel(Level level) {

		switch (level.ID) {
		case 1:
			currentPrefab = enemy1Prefab;
			break;
		case 2:
			currentPrefab = enemy2Prefab;
			break;
		case 3:
			currentPrefab = enemy3Prefab;
			break;
		case 4:
			currentPrefab = enemy4Prefab;
			break;
		default:
			currentPrefab = enemy4Prefab;
			break;
		}

		spawnsPerMinute = level.SpawnsPerMinute;
		totalSpawns += level.TotalSpawns;
		InvokeRepeating ("Spawn", 5, 60 / spawnsPerMinute);
	}

	void Spawn() {
		if (currentSpawns >= totalSpawns)
			CancelInvoke ();
		else {
			Vector3 spawnPoint;
			Vector3 playerPos;
			playerPos = playerPrefab.transform.position;
			Vector2 playerPos2D = new Vector2(playerPos.x, playerPos.y);
			Vector2 spawnPoint2D;

			do {
				spawnPoint = new Vector3 (
				Random.Range (minSpawnPointX, maxSpawnPointX), 
				Random.Range (minSpawnPointY, maxSpawnPointY), 
				transform.position.z);
				spawnPoint2D = new Vector2(spawnPoint.x, spawnPoint.y);
			} while (Vector2.Distance(playerPos2D, spawnPoint2D) < 10); 
			Instantiate (
			currentPrefab, 
			spawnPoint, 
			Quaternion.AngleAxis ((Mathf.Atan2 ((playerPrefab.transform.position.y - transform.position.y), 
		                                  (playerPrefab.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90), Vector3.forward));
			Globals.ZombieSpawned ();
			currentSpawns++;
		}
	}
}