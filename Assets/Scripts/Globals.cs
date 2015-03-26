using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Globals : object {

	private static object lockobj = new object();
	private static float _zombieKills = 0;
	private static float _totalZombies = 1;
	private static GameObject _player;
	private static GameObject _spawner;
	private static List<Ability> _abilities;
	private static List<Level> _levels;
	private static int currentLevel = 1;
	private static float killsForNextLevel;

	static Globals() {
		_levels = new List<Level> ();
		_levels.Add (new Level(0, 0, 0));
		for (int i = 1; i < 20; i++) {
			_levels.Add(new Level(i, i*10, i*10));
		}
		killsForNextLevel = _levels[currentLevel].TotalSpawns;
		//_levels.Add(new Level(1, 10, 10, (GameObject) Resources.Load("../PreFabs/AI1")));
		//_levels.Add(new Level(2, 20, 20));
		//_levels.Add(new Level(3, 30, 30));
		//_levels.Add(new Level(4, 40, 40));
	}

	public static float ZombieKills {
		get { return _zombieKills; }
	}

	public static float ZombiesAlive {
		get { return _totalZombies; }
	}

	public static List<Ability> Abilities {
		get { return _abilities; }
	}

	public static void AddNewAbility(Ability ability) {
		_abilities.Add (ability);
	}

	public static void ZombieKilled() {
		if (_player == null)
			_player = GameObject.Find ("Player");
		lock (lockobj) {
			_zombieKills++;
			_totalZombies--;
		}
		_player.SendMessage ("ZombieKilled");

		if (_zombieKills >= killsForNextLevel) {
			currentLevel++;
			_spawner.SendMessage ("SetLevel", _levels[currentLevel]);
			killsForNextLevel += _levels [currentLevel].TotalSpawns;
		}
	}

	public static void ZombieSpawned() {
		if (_spawner == null)
			_spawner = GameObject.Find ("Spawner");
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
