using UnityEngine;
using System.Collections;

public class PlayerMobillity : MonoBehaviour {
	public float speed;
	public GameObject bulletPrefab;
	public float fireRate;
	public Transform shotSpawn;
	public float coolDown;
	public Vector3 mousePosition;
	public float hitPoints;
	private bool firing = false;
	public AudioClip gunSound;
	void Update(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 moveDirection = (mousePosition - transform.position).normalized;
		
		float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);        

		
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;

		if (Input.GetMouseButtonDown (0)) {
			firing = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			firing = false;
		}

		if (firing && Time.time > coolDown) {
			coolDown = Time.time + fireRate;
			Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().PlayOneShot (gunSound);
		}
	}

	void HitWith(float damage) {
		if (hitPoints - damage > 0)
			hitPoints = hitPoints - damage;
		else
			Destroy (gameObject);
	}
}
