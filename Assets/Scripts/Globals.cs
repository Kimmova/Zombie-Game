using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Globals {

	private static object lockobj = new object();
	private static float _zombieKills = 0;
	private static float _totalZombies = 1;
	private static GameObject _player;
	private static List<Ability> _abilities;

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
