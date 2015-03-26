using UnityEngine;
using System.Collections;

public class Level {
	public int ID { get; set; }
	public float SpawnsPerMinute { get; set; }
	public float TotalSpawns { get; set; }

	public Level(int id, float spawnsPerMinute, float totalSpawns) {
		ID = id;
		SpawnsPerMinute = spawnsPerMinute;
		TotalSpawns = totalSpawns;
	}
}
