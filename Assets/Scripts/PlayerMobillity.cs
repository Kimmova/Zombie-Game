using UnityEngine;
using System.Collections;

public class PlayerMobillity : MonoBehaviour {

	public Camera followCamera;

	public float speed;
	public GameObject bulletPrefab;
	public float fireRate;
	public Transform shotSpawn;
	public float coolDown;
	public Vector3 mousePosition;
	private bool firing = false;

	void Update(){
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, 
		                                         Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);

		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;

		var newCamPos = transform.position;
		newCamPos.z = followCamera.transform.position.z;

		followCamera.transform.position = newCamPos;

		if (Input.GetMouseButtonDown (0)) {
			firing = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			firing = false;
		}

		if (firing && Time.time > coolDown) {
			coolDown = Time.time + fireRate;
			Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
		}
	}
	
}
