using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour {

	private float health;
	private float maxHealth;
	private PlayerMobillity player;
	List<int> ids;
	List<Ability> abilities;

	void Start() {
		player = (PlayerMobillity) FindObjectOfType (typeof(PlayerMobillity));
		maxHealth = player.hitPoints;
		ids = new List<int> ();
		abilities = Globals.AvailableAbilities();
	}

	void Update () {
		var newCamPos = player.transform.position;
		newCamPos.z = transform.position.z;
		
		transform.position = newCamPos;

		health = player.hitPoints;
	}

	void UpdateAbilities() {
		abilities = Globals.AvailableAbilities();
	}

	void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 20), "Health: ");
		GUI.Label (new Rect (100, 10, 100, 20), health + " (" + Mathf.Floor(health / maxHealth * 100f) + "%)");
		GUI.Label (new Rect (10, 30, 100, 20), "Kills: ");
		GUI.Label (new Rect (100, 30, 100, 20), Globals.ZombieKills.ToString());
		GUI.Label (new Rect (10, 50, 100, 20), "Zombies alive: ");
		GUI.Label (new Rect (100, 50, 100, 20), Globals.ZombiesAlive.ToString());
		float yPos = 70;

		foreach (Ability a in abilities) {
			if (ids.Contains(a.ID)) {
				GUI.Label (new Rect (10, yPos, 120, 20), "Ability " + a.ID + " - L" + a.Level + " - (" + a.ActivationKey.ToString() + "): ");
				GUI.Label (new Rect (120, yPos, 200, 20), player.AbilityReadyIn (a.ID, a.KillsRequired));
				yPos += 20;
			}
			else
				ids.Add(a.ID);
		}
	}
}
