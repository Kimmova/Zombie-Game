using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed;
	void Start () {
		rigidbody2D.velocity = transform.up * speed;
	}

	void Update(){

	}
	

}
