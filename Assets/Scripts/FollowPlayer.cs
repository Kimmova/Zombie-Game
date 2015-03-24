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
		GUI.Label (new Rect (10, 10, 50, 20), "Health: ");
		GUI.Label (new Rect (60, 10, 100, 20), health + " (" + Mathf.Floor(health / maxHealth * 100f) + "%)");
	}
}
