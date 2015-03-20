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

	void Start() {
		player = GameObject.Find ("Player").transform;
		previousSpeed = speed;
	}

	void FixedUpdate()
	{
		float z = Mathf.Atan2 ((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

		transform.eulerAngles = new Vector3 (0, 0, z);
		rigidbody2D.AddForce (gameObject.transform.up * speed);
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

	void HitWith(float damage) {
		if (hitPoints - damage > 0) 
			hitPoints = hitPoints - damage;
		else {
			Instantiate(bloodPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Destroy (gameObject);

		}
	}
}
