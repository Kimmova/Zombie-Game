using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;

	void Update(){
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}
}
