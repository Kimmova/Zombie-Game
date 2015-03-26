using UnityEngine;
using System.Collections;

public abstract class Ability {
	public int ID { get; set; }
	public float Cooldown { get; set; }
	public float Duration { get; set; }
	public float FireRateBoost { get; set; }
	public float ActionSpeed { get; set; }
	public bool Active { get; set; }
	public float KillsRequired { get; set; }

	public abstract void Use(GameObject player);
}
