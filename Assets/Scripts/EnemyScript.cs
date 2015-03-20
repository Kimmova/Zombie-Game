using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
	public Transform Player;
	public float hitPoints;
	public float damage;
	public float attackRate;
	public float coolDown;
	public GameObject bloodPrefab;

	void FixedUpdate()
	{
		float z = Mathf.Atan2 ((Player.transform.position.y - transform.position.y), (Player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

		transform.eulerAngles = new Vector3 (0, 0, z);
		rigidbody2D.AddForce (gameObject.transform.up * speed);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" && Time.time > coolDown) {
			coolDown = Time.time + attackRate;
			col.transform.SendMessage("HitWith", damage);
		}
	}

	void OnCollisionStay2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" && Time.time > coolDown) {
			coolDown = Time.time + attackRate;
			col.transform.SendMessage("HitWith", damage);
		}
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
