using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
	private float previousSpeed;
	private Transform player;
	public float hitPoints;
	public float damage;
	public float attackRate;
	public float coolDown;
	public GameObject bloodPrefab;
	public AudioClip zombieDeath;
	private float maxHitPoints;

	void Start() {
		player = GameObject.Find ("Player").transform;
		previousSpeed = speed;
		maxHitPoints = hitPoints;
	}
	
	void FixedUpdate()
	{
		float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

		transform.eulerAngles = new Vector3 (0, 0, z);
		GetComponent<Rigidbody2D>().AddForce (gameObject.transform.up * speed);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			speed = 0;
			if (Time.time > coolDown) {
				coolDown = Time.time + attackRate;
				col.transform.SendMessage("HitWith", damage); 
			}
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" && Time.time > coolDown) {
			coolDown = Time.time + attackRate;
			col.transform.SendMessage("HitWith", damage);
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		speed = previousSpeed;
	}

	void OnGUI()
	{
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);
		
		GUI.Box(new Rect(targetPos.x, Screen.height- targetPos.y, 60, 20), hitPoints + "/" + maxHitPoints);
	}

	void HitWith(float damage) {
		if (hitPoints - damage > 0) 
			hitPoints = hitPoints - damage;
		else {
			AudioSource.PlayClipAtPoint(zombieDeath, transform.position);
			Instantiate(bloodPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);
			((FollowPlayer) FindObjectOfType (typeof(FollowPlayer))).ZombieKilled();
		}
	}
}
