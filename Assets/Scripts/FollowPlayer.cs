using UnityEngine;
using System.Collections;

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
		GUI.Label (new Rect (10, 70, 100, 20), "Ability 1 (F): ");
		GUI.Label (new Rect (100, 70, 200, 20), player.Ability1ReadyIn());
	}
}
