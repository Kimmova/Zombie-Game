using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private float health;
	private float maxHealth;
	private PlayerMobillity player;
	private float zombieKills = 0;

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

	public void ZombieKilled() {
		zombieKills++;
	}

	void OnGUI () {
		GUI.Label (new Rect (10, 10, 50, 20), "Health: ");
		GUI.Label (new Rect (60, 10, 100, 20), health + " (" + Mathf.Floor(health / maxHealth * 100f) + "%)");
		GUI.Label (new Rect (10, 30, 50, 20), "Kills: ");
		GUI.Label (new Rect (60, 30, 100, 20), zombieKills.ToString());
	}
}
