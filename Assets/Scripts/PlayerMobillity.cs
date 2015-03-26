using UnityEngine;
using System.Collections;

public class PlayerMobillity : MonoBehaviour {
	public float speed;
	public GameObject bulletPrefab;
	public float fireRate;
	public Transform shotSpawn;
	public float coolDown;
	public Vector3 mousePosition;
	public float hitPoints;
	public float ability1CooldownTime;
	private float ability1Cooldown;
	public float ability1Duration;
	private float ability1EndTime;
	public float ability1FireRateBoost;
	public float ability1Speed;
	private bool ability1Active = false;
	public float ability1KillsRequired;
	public bool ability1Unlocked = false;
	private bool firing = false;
	private float zombieKills = 0;
	public AudioClip gunSound;
	public AudioClip PlayerDamage;

	void Start() {
		ability1Cooldown = Time.time;
	}

	void Update(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 moveDirection = (mousePosition - transform.position).normalized;


		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

		// Ability 1
		if (zombieKills >= ability1KillsRequired)
			ability1Unlocked = true;

		if (ability1Unlocked && Input.GetKeyDown (KeyCode.F) && ability1Cooldown <= Time.time) {
			ability1Active = true;
			ability1EndTime = Time.time + ability1Duration;
			ability1Cooldown = ability1EndTime + ability1CooldownTime;
			firing = true;
		}

		if (ability1Active) {
			if (ability1EndTime <= Time.time) {
				ability1Active = false;
				firing = false;
			}
			else
				transform.Rotate(0, 0, 10 * ability1Speed * Time.deltaTime);
		}
		else
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
			if (ability1Active)
				coolDown = Time.time + fireRate / ability1FireRateBoost;
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

	void Ability1(Quaternion rotation) {
		transform.rotation = rotation;
	}

	void ZombieKilled() {
		zombieKills++;
	}

	public string Ability1ReadyIn() {
		if (zombieKills < ability1KillsRequired)
			return "Locked (" + (ability1KillsRequired - zombieKills) + " more kills)";
		if (ability1Active)
			return "Active... " + Mathf.Floor(ability1EndTime - Time.time) + " s";
		if (ability1Cooldown > Time.time)
			return "Cooldown... " + Mathf.Floor(ability1Cooldown - Time.time).ToString() + " s";
		else
			return "Ready!";
	}
}
