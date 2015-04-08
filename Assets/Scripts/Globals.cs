using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Globals {

	private static object lockobj = new object();
	private static float _zombieKills = 0;
	private static float _totalZombies = 0;
	private static GameObject _player;
	private static GameObject _spawner;
	private static List<Ability> _abilities;
	private static List<Ability> _availableAbilities;
	private static List<Level> _levels;
	private static int currentLevel = 1;
	private static float killsForNextLevel;

	static Globals() {
		_abilities = new List<Ability> ();
		_availableAbilities = new List<Ability> ();
		_levels = new List<Level> ();
		_levels.Add (new Level(0, 0, 0));
		for (int i = 1; i < 20; i++) {
			_levels.Add(new Level(i, i*10, i*10));
		}
		killsForNextLevel = _levels[currentLevel].TotalSpawns;

		for (int i = 1; i < 3; i++) {
			var newAb = new Ability ();
			newAb.ID = 1;
			newAb.Cooldown = i * 60;
			newAb.Duration = i * 5;
			newAb.Level = i * 1;
			newAb.FireRateBoost = i * 25;
			newAb.ActionSpeed = i * 10;
			newAb.KillsRequired = i * 15;
			newAb.Unlocked = false;
			newAb.ActivationKey = KeyCode.F;
			_abilities.Add (newAb);
		}

		for (int i = 1; i < 3; i++) {
			var newAb = new Ability ();
			newAb.ID = 2;
			newAb.Cooldown = i * 60;
			newAb.Duration = i * 5;
			newAb.Level = i * 1;
			newAb.FireRateBoost = i * 25;
			newAb.ActionSpeed = i * 10;
			newAb.KillsRequired = i * 25;
			newAb.Unlocked = false;
			newAb.ActivationKey = KeyCode.G;
			_abilities.Add (newAb);
		}
	}

	public static float ZombieKills {
		get { return _zombieKills; }
	}

	public static float ZombiesAlive {
		get { return _totalZombies; }
	}

	public static List<Ability> Abilities() {
		var unique = _abilities.GroupBy(
			a => a.ID,
			(key, group) => group.First()
			).ToList();
		var availableAbilities = _abilities.Where (a => a.KillsRequired <= ZombieKills).ToList();
		availableAbilities.OrderByDescending (a => a.Level).GroupBy(
			a => a.ID,
			(key, group) => group.First()
			).ToList();;
		if (availableAbilities.Count > unique.Count)
			return availableAbilities;
		else
			return unique;
	}

	public static List<Ability> AvailableAbilities() {
		List<Ability> available = new List<Ability> ();
		foreach (Ability a in _abilities) {
			if (!available.Exists(av => av.ID == a.ID)) {
				available.Add(a);
			}
			else {
				var existing = available.Find(av => av.ID == a.ID);
				if (a.KillsRequired <= ZombieKills && a.Level > existing.Level) {
					available.Remove(existing);
					available.Add(a);
				}
			}
		}
		return available;
	}

	public static List<Level> Levels {
		get { return _levels; }
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

		foreach (Ability a in AvailableAbilities()) {
			if (_zombieKills >= a.KillsRequired)
				_player.SendMessage("AbilityUnlocked", a);
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
