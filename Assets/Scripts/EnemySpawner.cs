using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	private GameObject playerPrefab;
	public GameObject enemyPrefab;
	public float spawnsPerMinute;
	public int maxSpawnPointX;
	public int maxSpawnPointY;
	public int minSpawnPointX;
	public int minSpawnPointY;

	void Start() {
		InvokeRepeating ("Spawn", 5, 60 / spawnsPerMinute);
		playerPrefab = GameObject.Find ("Player").gameObject;
	}

	void Spawn() {
		Vector3 spawnPoint;
		do {
			spawnPoint = new Vector3 (
			Random.Range (minSpawnPointX, maxSpawnPointX), 
			Random.Range (minSpawnPointY, maxSpawnPointY), 
			0);
		} while (Vector3.Distance(playerPrefab.transform.position, spawnPoint) < 4); 
		Instantiate(
			enemyPrefab, 
			spawnPoint, 
			Quaternion.AngleAxis((Mathf.Atan2 ((playerPrefab.transform.position.y - transform.position.y), 
		                                  (playerPrefab.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90), Vector3.forward));
	}
}