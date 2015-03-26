using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	public float damage;
	public AudioClip BodyImpact;


	void Update(){
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
			col.transform.SendMessage("HitWith", damage);
			AudioSource.PlayClipAtPoint(BodyImpact, transform.position);
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Wall") {
			Destroy (gameObject);
		}
	}
}
