using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	public float damage;


	void Update(){
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			col.transform.SendMessage("HitWith", damage);
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
