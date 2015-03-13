using UnityEngine;
using System.Collections;

public class PlayerMobillity : MonoBehaviour {

	public float speed;
	public GameObject Bullet;
	public Transform shotSpawn;

	void Update(){
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, 
		                                         Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;

		if (Input.GetMouseButtonDown (0))
			Fire();
	}

	void Fire() 
	{
		Instantiate (Bullet, shotSpawn.position, shotSpawn.rotation);
	}
}
