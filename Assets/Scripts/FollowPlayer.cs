using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPlayer : MonoBehaviour {

	private float health;
	private float maxHealth;
	private PlayerMobillity player;

	void Start() {
		player = (PlayerMobillity) FindObjectOfType (typeof(PlayerMobillity));
		maxHealth = player.hitPoints;
	}

	void Update () {
		var newCamPos = player.transform.position;
		newCamPos.z = transform.position.z;
		
		transform.position = newCamPos;

		health = player.hitPoints;
	}

	void OnGUI () {
		GUI.Label (new Rect (10, 10, 100, 20), "Health: ");
		GUI.Label (new Rect (100, 10, 100, 20), health + " (" + Mathf.Floor(health / maxHealth * 100f) + "%)");
		GUI.Label (new Rect (10, 30, 100, 20), "Kills: ");
		GUI.Label (new Rect (100, 30, 100, 20), Globals.ZombieKills.ToString());
		GUI.Label (new Rect (10, 50, 100, 20), "Zombies alive: ");
		GUI.Label (new Rect (100, 50, 100, 20), Globals.ZombiesAlive.ToString());
		float yPos = 70;
		List<int> ids = new List<int>();
		foreach (Ability a in Globals.Abilities) {
			if (ids.Contains(a.ID)) {
				GUI.Label (new Rect (10, yPos, 100, 20), "Ability " + a.ID + " - L" + a.Level + " - (" + a.ActivationKey.ToString() + "): ");
				GUI.Label (new Rect (100, yPos, 200, 20), player.AbilityReadyIn (a.ID, a.KillsRequired));
				yPos += 20;
			}
			else
				ids.Add(a.ID);
		}
	}
}
