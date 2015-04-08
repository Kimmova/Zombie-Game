using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMobillity : MonoBehaviour {
	public float speed;
	public GameObject bulletPrefab;
	public float fireRate;
	public Transform shotSpawn;
	public float coolDown;
	public Vector3 mousePosition;
	public float hitPoints;
	List<Ability> unlockedAbilities;
	List<float> abilityCooldowns;
	private Ability activeAbility;
	private float abilityEndsAt;
	private bool firing = false;
	private float zombieKills = 0;
	public AudioClip gunSound;
	public AudioClip PlayerDamage;

	void Start() {
		unlockedAbilities = new List<Ability> ();
		abilityCooldowns = new List<float> ();
		activeAbility = null;
	}

	void Update(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 moveDirection = (mousePosition - transform.position).normalized;

		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		for (int i = 0; i < abilityCooldowns.Count; i++) {
			if (Input.GetKeyDown(unlockedAbilities[i].ActivationKey) && abilityCooldowns[i] <= Time.time) {
				ActivateAbility(unlockedAbilities[i], i);
			}
		}

		if (activeAbility != null) {
			if (abilityEndsAt <= Time.time) {
				activeAbility = null;
				firing = false;
			}
		}

		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;

		if (Input.GetMouseButtonDown (0)) {
			firing = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			firing = false;
		}

		if (firing && Time.time > coolDown) {
			if (activeAbility != null)
				coolDown = Time.time + fireRate / activeAbility.FireRateBoost;
			else
				coolDown = Time.time + fireRate;
			Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().PlayOneShot (gunSound);
		}
	}

	void HitWith(float damage) {

		GetComponent<AudioSource>().PlayOneShot (PlayerDamage);
		if (hitPoints - damage > 0)
			hitPoints = hitPoints - damage;
		else
			Destroy (gameObject);
	}

	void AbilityUnlocked(Ability newAbility) {
		if (!unlockedAbilities.Contains (newAbility)) {
			unlockedAbilities.Add (newAbility);
			abilityCooldowns.Add (Time.time);
		}
	}

	void ActivateAbility(Ability ability, int index) {
		switch (ability.ID) {
		case 1:
			firing = true;
			break;
		case 2:
			firing = true;
			break;
		default:
			return;
			break;
		}
		abilityCooldowns [index] = Time.time + ability.Duration + ability.Cooldown;
		abilityEndsAt = Time.time + ability.Duration;
		activeAbility = ability;
	}

	void ZombieKilled() {
		zombieKills++;
	}

	public string AbilityReadyIn(int abilityId, float killsReq) {
		int index = -1;
		for (int i = 0; i < unlockedAbilities.Count; i++) {
			if (unlockedAbilities[i].ID == abilityId) {
				index = i;
				break;
			}
		}
		if (index == -1 || unlockedAbilities[index] == null) 
			return "Locked (" + (killsReq - zombieKills) + " more kills)";

		if (zombieKills < unlockedAbilities[index].KillsRequired)
			return "Locked (" + (unlockedAbilities[index].KillsRequired - zombieKills) + " more kills)";
		if (activeAbility != null && activeAbility.ID == abilityId)
			return "Active... " + Mathf.Floor(abilityEndsAt - Time.time) + " s"; // ERROR HERE, FIX LATER!!!
		if (abilityCooldowns[index] > Time.time)
			return "Cooldown... " + Mathf.Floor(abilityCooldowns[index] - Time.time).ToString() + " s";
		else
			return "Ready!";
	}
}
