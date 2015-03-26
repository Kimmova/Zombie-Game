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
	public int maxSpawnPointX;
	public int maxSpawnPointY;
	public int minSpawnPointX;
	public int minSpawnPointY;
	private GameObject currentPrefab;

	void Start() {
		InvokeRepeating ("Spawn", 5, 60 / spawnsPerMinute);
		playerPrefab = GameObject.Find ("Player").gameObject;
		currentPrefab = enemy1Prefab;
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
		}

		spawnsPerMinute = level.SpawnsPerMinute;
		totalSpawns = level.TotalSpawns;
	}

	void Spawn() {
		Vector3 spawnPoint;
		Vector3 playerPos;
		playerPos = Camera.main.WorldToScreenPoint (playerPrefab.transform.position);
		playerPos.z = transform.position.z;

		do {
			spawnPoint = new Vector3 (
			Random.Range (minSpawnPointX, maxSpawnPointX), 
			Random.Range (minSpawnPointY, maxSpawnPointY), 
			transform.position.z);
		} while (Vector3.Distance(playerPos, spawnPoint) < 400); 
		Instantiate(
			currentPrefab, 
			spawnPoint, 
			Quaternion.AngleAxis((Mathf.Atan2 ((playerPrefab.transform.position.y - transform.position.y), 
		                                  (playerPrefab.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90), Vector3.forward));
		Globals.ZombieSpawned ();
	}
}