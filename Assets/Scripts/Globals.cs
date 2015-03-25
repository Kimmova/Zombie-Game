using UnityEngine;
using System.Collections;

public static class Globals {

	private static object lockobj = new object();
	private static float _zombieKills = 0;
	private static float _totalZombies = 1;
	private static GameObject player;

	public static float ZombieKills {
		get { return _zombieKills; }
	}

	public static float ZombiesAlive {
		get { return _totalZombies; }
	}

	public static void ZombieKilled() {
		if (player == null)
			player = GameObject.Find ("Player");
		lock (lockobj) {
			_zombieKills++;
			_totalZombies--;
		}
		player.SendMessage ("ZombieKilled");
	}

	public static void ZombieSpawned() {
		lock (lockobj) {
			_totalZombies++;
		}
	}

	public static bool MatchKills(float kills) {
		if (_zombieKills >= kills)
			return true;
		return false;
	}
}
