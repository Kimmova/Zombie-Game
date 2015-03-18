using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float speed;
	public Transform Player;

	void FixedUpdate()
	{
		float z = Mathf.Atan2 ((Player.transform.position.y - transform.position.y), (Player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;

		transform.eulerAngles = new Vector3 (0, 0, z);
		rigidbody2D.AddForce (gameObject.transform.up * speed);
	}
}
