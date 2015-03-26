using UnityEngine;
using System.Collections;

public class Ability {
	public int ID { get; set; }
	public int Level { get; set; }
	public float Cooldown { get; set; }
	public float Duration { get; set; }
	public float FireRateBoost { get; set; }
	public float ActionSpeed { get; set; }
	public bool Unlocked { get; set; }
	public float KillsRequired { get; set; }
	public KeyCode ActivationKey { get; set; }

	//public void Use(GameObject player);
}
